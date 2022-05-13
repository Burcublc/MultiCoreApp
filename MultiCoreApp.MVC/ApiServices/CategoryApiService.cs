using MultiCoreApp.MVC.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace MultiCoreApp.MVC.ApiServices
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;  //server'a bağlanırken httpprotokollerini kullanmak için httpclient'larla bağlanıyoruz

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> categoryDtos;
            var response = await _httpClient.GetAsync("category"); //bunun için mvc'inin appsettings.json'a git baseurl'i ver
            //sonra program.cs'e git
            if(response.IsSuccessStatusCode)//geri dönüş değeri varmı yokmu diye kontrol etmek için
            {
                //IsSuccessStatusCode gelen datayı true'a çevirir
                categoryDtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync())!;  //veriyi string olarak oku ve CategoryDto tipine çevir
            }//json veriyi şifreli olarak getirir o yüzden bu gelen veri yani response içindeki Id ve Name CategoryDto'dada var o yüzden veriyi string olarak oku sonra onu categorydto olarak al ve categorydto odaki aynı propertylere bu verileri ata
            else
            {
                categoryDtos = null!;
            }
            return categoryDtos;
        }
        public async Task<CategoryDto> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"category/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                return null!;
            }
        }
        public async Task<CategoryDto> AddAsync(CategoryDto catDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(catDto),Encoding.UTF8,"application/json");//json tipine dönüştürmek için serialized kullandık
            var response = await _httpClient.PostAsync("category", stringContent);
            if (response.IsSuccessStatusCode)
            {
                catDto=JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync())!;
                return catDto;
            }
            else
            {
                return null!;
            }
        }
        public async Task<bool> Update(CategoryDto catDto)
        {
            var stringContent=new StringContent(JsonConvert.SerializeObject(catDto), Encoding.UTF8, "application/json"); //medyatype parametresi bu uygulamada json veri  tipinde gelicek diyor 
            var response = await _httpClient.PutAsync($"category",stringContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
