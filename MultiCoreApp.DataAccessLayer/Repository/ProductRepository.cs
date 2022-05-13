using Microsoft.EntityFrameworkCore;
using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.DataAccessLayer.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private MultiDbContext MultiDbContext { get => _db as MultiDbContext; }
        public ProductRepository(MultiDbContext db) : base(db)
        {

        }

        public async Task<Product> GetWithCategoryByIdAsync(Guid proId)
        {
            var product = _db.Products.Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == proId);
            return (await product)!;
            
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            var products = MultiDbContext.Products.Include(s => s.Category).ToListAsync();
            return (await products);
        }
    }
}
