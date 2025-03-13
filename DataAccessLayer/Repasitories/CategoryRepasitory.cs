using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repasitories
{
    public class CategoryRepasitory : Repasitory<Category>, ICategoryInterface
    {
        public CategoryRepasitory(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoryWithBooksAsync()
        {
            var categories = await _appDbContext.Categories.Include(c => c.Books).ToListAsync();
            return categories;
        }
    }
}
