using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.IntUnitOfWork;
using MultiCoreApp.DataAccessLayer;
using MultiCoreApp.DataAccessLayer.Repository;
using MultiCoreApp.DataAccessLayer.UnitOfWork;
using MultiCoreApp.MVC.ApiServices;
using MultiCoreApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<CategoryApiService>(opt=>
{
    opt.BaseAddress = new Uri(builder.Configuration["baseUrl"]); //base url httpclient sınıfından gelir
});//bunu kullanacağımı declare'e ediyorum
builder.Services.AddHttpClient<ProductApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["baseUrl"]);
});
// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddScoped<CategoryNotFoundFilter>();

builder.Services.AddDbContext<MultiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr").ToString(), sqlServerOptionsAction: sqloptions =>
    {
        sqloptions.EnableRetryOnFailure();

        sqloptions.MigrationsAssembly("MultiCoreApp.DataAccessLayer");
    });
    options.EnableSensitiveDataLogging();
});
//Life-cycle ==> Iliskili kodun yasam suresi
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
