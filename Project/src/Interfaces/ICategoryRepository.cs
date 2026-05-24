using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesOrderedByName();
    }
}
