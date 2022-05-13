using MultiCoreApp.Core.Models;

namespace MultiCoreApp.API.DTOs
{
    public class ProductWithCategoryDto:ProductDto
    {
        public  CategoryDto Category { get; set; }
    }
}
