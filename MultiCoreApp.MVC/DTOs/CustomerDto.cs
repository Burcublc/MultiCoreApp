using System.ComponentModel.DataAnnotations;

namespace MultiCoreApp.MVC.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} alani zorunludur")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "{0} alani zorunludur")]
        public string Email { get; set; }
        public string City { get; set; }
    }
}
