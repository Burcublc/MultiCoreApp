using AutoMapper;
using MultiCoreApp.API.DTOs;
using MultiCoreApp.Core.Models;

namespace MultiCoreApp.API.Mapping
{
    public class MapProfile : Profile //AutoMapper'ın getirdiği bri sınıfı miras olarak alıyoruz 
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryDto>(); //Category'i CategoryDto 'ya dönüştür
            CreateMap<CategoryDto,Category>(); //CategoryDto 'yu Category 'e çevir

            CreateMap<Category, CategoryWithProductsDto>();
            CreateMap<CategoryWithProductsDto, Category>();

            CreateMap<Product, ProductDto>(); 
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<ProductWithCategoryDto, Product>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto,Customer>();

        }
    }
}
