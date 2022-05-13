using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiCoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.DataAccessLayer.Seeds
{
    public class CustomerSeed : IEntityTypeConfiguration<Customer>
    {
        private readonly Guid[] _guids;

        public CustomerSeed(Guid[] guids)
        {
            _guids = guids;
        }

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer { Id = _guids[0], Name = "Burcu BALCI", Address= "Aydinli mah. İstanbul", Phone= "02456546", Email= "burcu@gmail.com", City= "İstanbul"},
                new Customer { Id = _guids[1], Name = "Ozan BALCI", Address = "Aydinli mah. İstanbul", Phone = "0165454", Email = "ozan@gmail.com", City = "İstanbul"}
                );
        }
    }
}
