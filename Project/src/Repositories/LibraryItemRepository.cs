using Microsoft.EntityFrameworkCore;
using Project.src.Enums;
using Project.src.Models;

namespace Project.src.Repositories
{
    public class LibraryItemRepository : GenericRepository<LibraryItem>
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

            if (resultList.Count == 0)
                throw new InvalidOperationException($"No library item found with title containing: {title}");

            return resultList;
        }

        //This method retrieves all library items that are currently available. 
        public IEnumerable<LibraryItem> GetAvailableItems()
        {
            var availableItems = _context.LibraryItems
                .AsNoTracking()
                .Where(li => li.Status == ItemStatus.Available)
                .ToList();

            if (availableItems.Count == 0)
                throw new InvalidOperationException("No available library items found.");

            return availableItems;
        }

        //This method retrieves all library items ordered by their title in ascending order. 
        public IEnumerable<LibraryItem> GetItemsOrderedByTitle()
        {
            var orderedItems = _context.LibraryItems
                .AsNoTracking()
                .OrderBy(li => li.Title)
                .ToList();

            if (orderedItems.Count == 0)
                throw new InvalidOperationException("No library items found.");

            return orderedItems;
        }

        //This method retrieves all library items that belong to a specific category, identified by the categoryId parameter. 
        public IEnumerable<LibraryItem> GetItemsByCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category id must be positive.", nameof(categoryId));

            var items = _context.LibraryItems
                .AsNoTracking()
                .Where(li => li.CategoryId == categoryId)
                .ToList();

            if (items.Count == 0)
                throw new InvalidOperationException("No library items found in this category.");

            return items;
        }
    }
}