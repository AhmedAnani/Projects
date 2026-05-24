using Microsoft.EntityFrameworkCore;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Repositories
{
    public class CategoryRepository:GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        //This method is used to get all categories with their names
        public IEnumerable<Category> GetCategoriesOrderedByName()
        {
            return _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
