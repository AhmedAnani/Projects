using System;
using System.Collections.Generic;
using System.Linq;
using Project.src.Models;

namespace Project.src.Interfaces
{
    public interface ILibraryItemService
    {
        string AddBook(User? currentUser, string title, int categoryId, string author, string description);

        string AddEBook(User? currentUser, string title, int categoryId, string author, string description, string fileSize);

        string AddMagazine(User? currentUser, string title, int categoryId);
        string UpdateBook(User? currentUser, int itemId, string title, int categoryId, string author, string description);

        string UpdateEBook(User? currentUser, int itemId, string title, int categoryId, string author, string description, string fileSize);

        string UpdateMagazine(User? currentUser, int itemId, string title, int categoryId);

        string RemoveItem(User? currentUser, int itemId);

        IEnumerable<LibraryItem> GetAllItems(User? currentUser);

        LibraryItem? GetItemById(User? currentUser, int itemId);

        IEnumerable<LibraryItem> SearchByTitle(User? currentUser, string title);

        IEnumerable<LibraryItem> GetAvailableItems(User? currentUser);

        IEnumerable<LibraryItem> GetItemsOrderedByTitle(User? currentUser);

        IEnumerable<LibraryItem> GetItemsByCategory(User? currentUser, int categoryId);

        string AddCategory(User? currentUser, string name);

        string UpdateCategory(User? currentUser, int categoryId, string name);

        string DeleteCategory(User? currentUser, int categoryId);

        IEnumerable<Category> GetCategoriesOrderedByName(User? currentUser);


    }
}