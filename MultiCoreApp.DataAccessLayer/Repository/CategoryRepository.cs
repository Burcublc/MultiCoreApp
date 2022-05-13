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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private MultiDbContext MultiDbContext { get => _db as MultiDbContext; }
        public CategoryRepository(MultiDbContext db) : base(db)
        {
        }

        public async Task<Category> GetWithProductByIdAsync(Guid catId)
        {
            return (await _db.Categories.Include(s => s.Products).FirstOrDefaultAsync(s => s.Id == catId))!;
        }
    }
}
