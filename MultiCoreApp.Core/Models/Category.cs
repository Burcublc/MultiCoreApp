using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.Core.Models
{

    public class Category
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Product> Products { get; set; } //virtual;lazy loading 
    }
}
