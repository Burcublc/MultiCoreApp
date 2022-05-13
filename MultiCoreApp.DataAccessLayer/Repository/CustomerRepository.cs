using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.DataAccessLayer.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        private MultiDbContext MultiDbContext { get => _db as MultiDbContext; }
        public CustomerRepository(MultiDbContext db) : base(db)
        {
        }
    }
}
