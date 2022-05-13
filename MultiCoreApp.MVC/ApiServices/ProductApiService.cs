using MultiCoreApp.MVC.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace MultiCoreApp.MVC.ApiServices
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductWithCategoryDto>> GetAllAsync()
        {
            IEnumerable<ProductWithCategoryDto> productDtos;
            var response = await _httpClient.GetAsync("product");
            if(response.IsSuccessStatusCode)
            {
                productDtos=JsonConvert.DeserializeObject<IEnumerable<ProductWithCategoryDto>>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                productDtos = null!;
            }
            return productDtos;
        }
        public async Task<ProductWithCategoryDto> AddAsync(ProductWithCategoryDto proDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(proDto), Encoding.UTF8, "application/json");//json tipine dönüştürmek için serialized kullandık
            var response = await _httpClient.PostAsync("product", stringContent);
            if (response.IsSuccessStatusCode)
            {
                proDto = JsonConvert.DeserializeObject<ProductWithCategoryDto>(await response.Content.ReadAsStringAsync())!;
                return proDto;
            }
            else
            {
                return null!;
            }
        }
        public async Task<ProductWithCategoryDto> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"product/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductWithCategoryDto>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                return null!;
            }
        }
        public async Task<bool> Update(ProductWithCategoryDto proDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(proDto), Encoding.UTF8, "application/json"); //medyatype parametresi bu uygulamada json veri  tipinde gelicek diyor 
            var response = await _httpClient.PutAsync($"product", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<ProductWithCategoryDto>> GetAllWithCategoryAsync()
        {
            IEnumerable<ProductWithCategoryDto> productDtos;
            var response = await _httpClient.GetAsync("product/categoryall");
            if (response.IsSuccessStatusCode)
            {
                productDtos = JsonConvert.DeserializeObject<IEnumerable<ProductWithCategoryDto>>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                productDtos = null!;
            }
            return productDtos;
        }
        public async Task<ProductWithCategoryDto> GetByIdForDetail(Guid id)
        {
            var response = await _httpClient.GetAsync($"product/{id}/category");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductWithCategoryDto>(await response.Content.ReadAsStringAsync())!;
            }
            else
            {
                return null!;
            }
        }
    }
}
