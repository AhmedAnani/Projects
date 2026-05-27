using Project.src.Exceptions;
using Project.src.Interfaces;
using Project.src.Models;
using System;

namespace Project.src.Controller
{
    public class LibraryItemsManager
    {
        private readonly ILibraryItemService _libraryItemService;

        //Make Dependancy Injection for ILibraryItemService inside the constructor
        public LibraryItemsManager(ILibraryItemService libraryItemService)
        {
            _libraryItemService = libraryItemService ?? throw new ArgumentNullException(nameof(libraryItemService));
        }

        //Method for managing AddBook
        public void AddBook(User? currentUser, string title, int categoryId, string author, string description)
        {
            try
            {
                string message = _libraryItemService.AddBook(currentUser, title, categoryId, author, description);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing AddEBook
        public void AddEBook(User? currentUser, string title, int categoryId, string author, string description, string fileSize)
        {
            try
            {
                string message = _libraryItemService.AddEBook(currentUser, title, categoryId, author, description, fileSize);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing AddMagazine
        public void AddMagazine(User? currentUser, string title, int categoryId)
        {
            try
            {
                string message = _libraryItemService.AddMagazine(currentUser, title, categoryId);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing UpdateBook
        public void UpdateBook(User? currentUser, int itemId, string title, int categoryId, string author, string description)
        {
            try
            {
                string message = _libraryItemService.UpdateBook(currentUser, itemId, title, categoryId, author, description);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing UpdateEBook
        public void UpdateEBook(User? currentUser, int itemId, string title, int categoryId, string author, string description, string fileSize)
        {
            try
            {
                string message = _libraryItemService.UpdateEBook(currentUser, itemId, title, categoryId, author, description, fileSize);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing UpdateMagazine
        public void UpdateMagazine(User? currentUser, int itemId, string title, int categoryId)
        {
            try
            {
                string message = _libraryItemService.UpdateMagazine(currentUser, itemId, title, categoryId);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing DeleteItems
        public void RemoveItem(User? currentUser, int itemId)
        {
            try
            {
                string message = _libraryItemService.RemoveItem(currentUser, itemId);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for Showing All items in the Library System
        public void ShowAllItems(User? currentUser)
        {
            try
            {
                var items = _libraryItemService.GetAllItems(currentUser);
                LibraryItemConsolePrinter.LibraryItems(items);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for return existing item from his id
        public void ShowItemById(User? currentUser, int itemId)
        {
            try
            {
                var item = _libraryItemService.GetItemById(currentUser, itemId);

                if (item == null)
                {
                    ConsolePrinter.Warning("Item not found.");
                    return;
                }

                LibraryItemConsolePrinter.LibraryItemInfo(item);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for searching for item by his title
        public void SearchByTitle(User? currentUser, string title)
        {
            try
            {
                var items = _libraryItemService.SearchByTitle(currentUser, title);
                LibraryItemConsolePrinter.LibraryItems(items);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for showing all avilable items in the Library System
        public void ShowAvailableItems(User? currentUser)
        {
            try
            {
                var items = _libraryItemService.GetAvailableItems(currentUser);
                LibraryItemConsolePrinter.LibraryItems(items);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for returning Items by its Gategories
        public void ShowItemsByCategory(User? currentUser, int categoryId)
        {
            try
            {
                var items = _libraryItemService.GetItemsByCategory(currentUser, categoryId);
                LibraryItemConsolePrinter.LibraryItems(items);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for returning Items orderd by its titles
        public void ShowItemsOrderedByTitle(User? currentUser)
        {
            try
            {
                var items = _libraryItemService.GetItemsOrderedByTitle(currentUser);
                LibraryItemConsolePrinter.LibraryItems(items);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing AddCategory
        public void AddCategory(User? currentUser, string name)
        {
            try
            {
                string message = _libraryItemService.AddCategory(currentUser, name);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for managing UpdateCategory
        public void UpdateCategory(User? currentUser, int categoryId, string name)
        {
            try
            {
                string message = _libraryItemService.UpdateCategory(currentUser, categoryId, name);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }


        //Method for managing DeleteCategory
        public void DeleteCategory(User? currentUser, int categoryId)
        {
            try
            {
                string message = _libraryItemService.DeleteCategory(currentUser, categoryId);
                ConsolePrinter.Success(message);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for Showing all categories inside the Library System
        public void ShowCategoriesOrderedByName(User? currentUser)
        {
            try
            {
                var categories = _libraryItemService.GetCategoriesOrderedByName(currentUser);
                CategoryConsolePrinter.Categories(categories);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }


        //Method for handling Exceptions
        private static void PrintError(Exception ex)
        {
            if (ex is UnauthorizedAccessException ||
                ex is ArgumentException ||
                ex is KeyNotFoundException ||
                ex is CategoryNotExistException ||
                ex is InvalidOperationException)
            {
                ConsolePrinter.Error(ex.Message);
                return;
            }

            ConsolePrinter.Error($"Unexpected error: {ex.Message}");
        }
    }
}
