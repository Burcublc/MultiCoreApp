using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiCoreApp.API.Extensions;
using MultiCoreApp.API.Filters;
using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.IntUnitOfWork;
using MultiCoreApp.DataAccessLayer;
using MultiCoreApp.DataAccessLayer.Repository;
using MultiCoreApp.DataAccessLayer.UnitOfWork;
using MultiCoreApp.Service.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));//automapperımın programda çalışmasını istiyorum amacımız burda kullancağımız servisleri tetiklemek
builder.Services.AddScoped<CategoryNotFoundFilter>();
// Add services to the container.
//burda database'imiz için service ekledik
//burarada nugette Microsoft.EntityFrameworkCore.Design'ı bu katmana ve data access layer'a ekledik
builder.Services.AddDbContext<MultiDbContext>(options=>
{
    //git nugetpacket manager'a bu katmana Microsoft.EntityFrameworkCore.SqlServer'ı ekle
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr").ToString(),sqlServerOptionsAction: sqloptions =>
    {
        sqloptions.EnableRetryOnFailure(); //bir hata bulursa diye arka planda
        sqloptions.MigrationsAssembly("MultiCoreApp.DataAccessLayer");//burda migration dosyalarının oluşturacağı yerin yolunu veriyoruz
    });
});
//life-cycle=>ilişkili kodu yaşam süresi
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//AddScope:request ile response arasında geçen süre;bir kere repository oluptur bir daha oluşturma;başlangıç ve bitiş belli olduğu için
//builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));//istek ve sonuçta üç kere mesela newlendi üç kere repository oluşturur
//builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));//veri değişimi bir kere olduğu için repository bir kez oluşur
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<ICategoryService, CategoryService>(); //burda ICategoryService 'i CategoryService' dönüştürüyoruz
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
//repository işlemlerinde addscoped kullanılır
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builderer =>
        {
            builderer.WithOrigins("http://localhost:3000");
        });
});
builder.Services.AddControllers(o =>
{
    o.Filters.Add(new ValidationFilter()); //bütün controllerlarda bu filterı artık kullanabilirim
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;  //arık tüm controllerımın için validationfilter çalışacak 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//Middleware Katmanı:
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomException(); //usecustomexcepiton'ı çalıştır

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins); //bunun d-sayesinde bunu nerde ve ne zaman tetikleneceğini bildiriyoruz

app.UseAuthorization();

app.MapControllers();

app.Run();
