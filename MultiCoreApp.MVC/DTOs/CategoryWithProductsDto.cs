namespace MultiCoreApp.MVC.DTOs
{
    public class CategoryWithProductsDto:CategoryDto
    {
        //bu sınıfta bizim bir product listesine ihtiyacımız olduğu için controller'daki GetWithProductById metodu için bu classı oluşturduk
        public ICollection<ProductDto> Products { get; set; }
    }
}
