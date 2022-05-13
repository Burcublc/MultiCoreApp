using Microsoft.EntityFrameworkCore;
using MultiCoreApp.Core.Models;
using MultiCoreApp.DataAccessLayer.Configurations;
using MultiCoreApp.DataAccessLayer.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.DataAccessLayer
{
    public class MultiDbContext : DbContext //base:DbContext ;ayrıca git nugete microsoft.entityframework.core'u ekle
    {
        public MultiDbContext(DbContextOptions<MultiDbContext> options):base(options) //options burda connection string 'imin name'i
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Customer> Customers { get; set; }
        //apinin içerisindeki appsettings.json 'a git 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid g1=Guid.NewGuid();
            Guid g2=Guid.NewGuid();
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            modelBuilder.ApplyConfiguration(new ProductSeed(new Guid[] { g1,g2 }));
            modelBuilder.ApplyConfiguration(new CategorySeed(new Guid[] { g1,g2 }));
            modelBuilder.ApplyConfiguration(new CustomerSeed(new Guid[] { g1,g2 }));
        }
    }
}
