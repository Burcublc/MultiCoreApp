using Microsoft.AspNetCore.Diagnostics;
using MultiCoreApp.API.DTOs;
using Newtonsoft.Json;

namespace MultiCoreApp.API.Extensions
{
    public static class UseCustomExtensionHandler
    {
        //extensionlar static olmak zorundadı
        //belli bir görevi infa etmek, araya giricek ve çıkıcak yani program.cs içerisinde middleware olarak kullanıcaz
        public static void UseCustomException(this IApplicationBuilder app) //bunu applicationbuilder'da kullanıcağımı söylüyorum
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    //sunucu hatası olduğu için
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/Json"; //ben sistemdeki 500 hatasını yakalıyp kendi 500 hatama çeviriyorum
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        if (ex != null)
                        {
                            ErrorDto errorDto = new ErrorDto();
                            errorDto.Status = 500;
                            errorDto.Errors.Add(ex.Message+"Hata Kodunu Yakaladik!"); //classta oluşturdum veriyi json'a çeviryorum
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto)); //burda
                        }
                    }
                });
            });
        }
    }
}
