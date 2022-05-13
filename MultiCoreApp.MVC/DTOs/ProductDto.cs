using System.ComponentModel.DataAnnotations;

namespace MultiCoreApp.MVC.DTOs
{
    public class ProductDto
    {
        //kullanıcının göreceği alan olucak
        public Guid Id { get; set; }

        [Required(ErrorMessage ="{0} alani zorunludur")]
        public string Name { get; set; }

        [Range(1,double.MaxValue,ErrorMessage ="{0} alani 0'dan büyük olmalidir")]
        public int Stock { get; set; } 

        [Range(1, double.MaxValue, ErrorMessage = "{0} alani 0'dan büyük olmalidir")]
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }


    }
}
