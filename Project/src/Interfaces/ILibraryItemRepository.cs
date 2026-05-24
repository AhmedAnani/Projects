using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface ILibraryItemRepository:IRepository<LibraryItem>
    {
        IEnumerable<LibraryItem> SearchByTitle(string title);
        IEnumerable<LibraryItem> GetAvailableItems();
        IEnumerable<LibraryItem> GetItemsOrderedByTitle();
        IEnumerable<LibraryItem> GetItemsByCategory(int categoryId);
    }
}
