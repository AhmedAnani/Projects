using Microsoft.EntityFrameworkCore;
using Project.src.Enums;
using Project.src.Interfaces.IRepository;
using Project.src.Models;

namespace Project.src.Repositories
{
    public class LibraryItemRepository : GenericRepository<LibraryItem>,ILibraryItemRepository
    {
        public LibraryItemRepository(AppDbContext context) : base(context)
        {
        }

        // These methods are specific to LibraryItem and not part of the generic repository interface

        //This method used to Search for library items by title, it performs a case-insensitive search and returns all matching items. If no items are found, it throws an exception.
        public IEnumerable<LibraryItem> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            var searchText = title.Trim().ToLower();

            var resultList = _context.LibraryItems
                .AsNoTracking()
                .Include(li => li.Category)
                .Where(li => li.Title.ToLower().Contains(searchText))
                .ToList();
 
            return resultList;
        }

        //This method retrieves all library items that are currently available. 
        public IEnumerable<LibraryItem> GetAvailableItems()
        {
            var availableItems = _context.LibraryItems
                .Include(li => li.Category)
                .AsNoTracking()
                .Where(li => li.Status == ItemStatus.Available)
                .ToList();

            return availableItems;
        }

        //This method retrieves all library items ordered by their title in ascending order. 
        public IEnumerable<LibraryItem> GetItemsOrderedByTitle()
        {
            var orderedItems = _context.LibraryItems
                .Include(li => li.Category)
                .AsNoTracking()
                .OrderBy(li => li.Title)
                .ToList();

            return orderedItems;
        }

        //This method retrieves all library items that belong to a specific category, identified by the categoryId parameter. 
        public IEnumerable<LibraryItem> GetItemsByCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category id must be positive.", nameof(categoryId));

            var items = _context.LibraryItems
                .Include(li => li.Category)
                .AsNoTracking()
                .Where(li => li.CategoryId == categoryId)
                .ToList();     

            return items;
        }
    }
}