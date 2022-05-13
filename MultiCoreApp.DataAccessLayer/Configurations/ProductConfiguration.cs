using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiCoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.DataAccessLayer.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            //builder.Property(s => s.Id).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.Stock).IsRequired();
            builder.HasOne(p=>p.Category).WithMany(c=>c.Products).HasForeignKey(p=>p.CategoryId); //burda foreignkey bağlantısını yaptık
            builder.ToTable("tblProducts");
        }
    }
}
