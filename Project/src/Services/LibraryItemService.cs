using Project.src.Exceptions;
using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Project.src.Services
{
    public class LibraryItemService : ILibraryItemService
    {
        private readonly ILibraryItemRepository _libraryItemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorizationService _authorizationService;

        // Constructor to inject dependencies
        public LibraryItemService(ILibraryItemRepository libraryItemRepository, ICategoryRepository categoryRepository, IAuthorizationService authorizationService)
        {
            //To ensure that the service cannot be instantiated without providing the necessary dependencies, we throw an exception if either of them is null.
            _libraryItemRepository = libraryItemRepository?? throw new ArgumentNullException(nameof(libraryItemRepository));
            _categoryRepository = categoryRepository?? throw new ArgumentNullException(nameof(categoryRepository));
            _authorizationService = authorizationService?? throw new ArgumentNullException(nameof(authorizationService));
        }

        //This Service method is responsible for adding a new book to the library.
        public string AddBook(User? currentUser, string title, int categoryId, string author, string description)
        {
            // Check if the user is authorized to add items
            EnsureCanAddItems(currentUser);

            //create a new book object to add to the library
            var book = new Book(title, categoryId, author, description);

            try
            {
                //Check if the category exists before adding the book
                EnsureCategoryExists(categoryId);


                //Add the book to the library
                _libraryItemRepository.Add(book);
                return $"Book {book.Title} added successfully.";
            }
            catch (CategoryNotExistException)
            {
                throw; // Re-throw the exception
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the book.", ex);
            }
        }

        //This Service method is responsible for adding a new ebook to the library.
        public string AddEBook(User? currentUser, string title, int categoryId, string author, string description, string fileSize)
        {
            // Check if the user is authorized to add items
            EnsureCanAddItems(currentUser);

            //create a new Ebook object to add to the library
            var ebook = new EBook(title, categoryId, author, description, fileSize);

            try
            {
                //Check if the category exists before adding the ebook
                EnsureCategoryExists(categoryId);
                //Add the ebook to the library
                _libraryItemRepository.Add(ebook);
                return $"EBook {ebook.Title} added successfully.";
            }
            catch (CategoryNotExistException)
            {
                throw; // Re-throw the exception
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the ebook.", ex);
            }

        }

        //This Service method is responsible for adding a new magazine to the library.
        public string AddMagazine(User? currentUser, string title, int categoryId)
        {
            // Check if the user is authorized to add items
            EnsureCanAddItems(currentUser);

            //create a new Magazine object to add to the library
            var magazine = new Magazine(title, categoryId);

            try 
            {
                //Check if the category exists before adding the magazine
                EnsureCategoryExists(categoryId);
                //Add the magazine to the library
                _libraryItemRepository.Add(magazine);
                return $"Magazine {magazine.Title} added successfully.";
            }
            catch (CategoryNotExistException)
            {
                throw; // Re-throw the exception
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the magazine.", ex);
            }
        }

        //This Service method is responsible for updating an existing book in the library.
        public string UpdateBook(User? currentUser, int itemId, string title, int categoryId, string author, string description)
        {
            // Check if the user is authorized to update items
            EnsureCanUpdateItems(currentUser);

            //Ensure valid itemId
            EnsureValidItemId(itemId);

            //Ensure valid categoryId
            EnsureValidCategoryId(categoryId);

            //create a new book object with the updated information
            var Updatedbook = new Book(title, categoryId, author, description);

            try 
            {
                //Check if the category exists before updating the book
                EnsureCategoryExists(categoryId);

                _libraryItemRepository.Update(itemId, item =>
                {
                    if (item is not Book book)
                        throw new InvalidOperationException("The selected item is not a book.");

                    //Update the book's properties with the new values
                    book.Rename(Updatedbook.Title);
                    book.ChangeCategory(Updatedbook.CategoryId);
                    book.ChangeAuthor(Updatedbook.Author);
                    book.ChangeDescription(Updatedbook.Description);
                });

                return $"Book '{Updatedbook.Title}' updated successfully.";
            }

            //For throwing the exception inside the repository when the item to be updated does not exist
            catch (KeyNotFoundException)
            {
                throw;
            }

            catch (CategoryNotExistException)
            {
                throw; // Re-throw the exception The selected item is not a book.
            }
            //For Exception that 
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the book.", ex);
            }

        }

        //This Service method is responsible for updating an existing ebook in the library.
        public string UpdateEBook(User? currentUser, int itemId, string title, int categoryId, string author, string description, string fileSize)
        {
            // Check if the user is authorized to update items
            EnsureCanUpdateItems(currentUser);

            //Ensure valid itemId
            EnsureValidItemId(itemId);

            //Ensure valid categoryId
            EnsureValidCategoryId(categoryId);

            //create a new Ebook object with the updated information
            var UpdatedEbook = new EBook(title, categoryId, author, description, fileSize);
            try 
            {
                //Check if the category exists before updating the book
                EnsureCategoryExists(categoryId);

                _libraryItemRepository.Update(itemId, item =>
                {
                    //Check if the item is an ebook before updating
                    if (item is not EBook ebook)
                        throw new InvalidOperationException("The selected item is not an ebook.");

                    //Update the ebook's properties with the new values
                    ebook.Rename(UpdatedEbook.Title);
                    ebook.ChangeCategory(UpdatedEbook.CategoryId);
                    ebook.ChangeAuthor(UpdatedEbook.Author);
                    ebook.ChangeDescription(UpdatedEbook.Description);
                    ebook.ChangeFileSize(UpdatedEbook.FileSize);
                    
                });

                return $"EBook '{UpdatedEbook.Title}' updated successfully.";
            }

            //For throwing the exception inside the repository when the item to be updated does not exist
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (CategoryNotExistException)
            {
                throw; // Re-throw the exception The selected item is not a book.
            }
            //For Exception that 
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the ebook.", ex);
            }
        }

        //This Service method is responsible for updating an existing magazine in the library.
        public string UpdateMagazine(User? currentUser, int itemId, string title, int categoryId)
        {
            // Check if the user is authorized to update items
            EnsureCanUpdateItems(currentUser);

            //Ensure valid itemId
            EnsureValidItemId(itemId);

            //Ensure valid categoryId
            EnsureValidCategoryId(categoryId);

            //create a new Magazine object with the updated information
            var UpdatedMagazine = new Magazine(title, categoryId);

            try
            {
                //Check if the category exists before updating the magazine
                EnsureCategoryExists(categoryId);

                _libraryItemRepository.Update(itemId, item =>
                {
                    //Check if the item is a magazine before updating
                    if(item is not Magazine magazine)
                        throw new InvalidOperationException("The selected item is not a magazine.");

                    //Update the magazine's properties with the new values
                    magazine.Rename(UpdatedMagazine.Title);
                    magazine.ChangeCategory(UpdatedMagazine.CategoryId);
                });
                return $"Magazine '{UpdatedMagazine.Title}' updated successfully.";
            }

            //For throwing the exception inside the repository when the item to be updated does not exist
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (CategoryNotExistException)
            {
                throw; // Re-throw the exception The selected item is not a book.
            }
            //For Exception that 
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the magazine.", ex);
            }
        }

        //This Service method is responsible for removing an existing item from the library.
        public string RemoveItem(User? currentUser, int itemId)
        {
            // Check if the user is authorized to remove items
            EnsureCanDeleteItems(currentUser);

            //Ensure valid itemId
            EnsureValidItemId(itemId);

            try
            {
                //Get the item to be removed to check if it exists before attempting to remove it
                var item = _libraryItemRepository.GetById(itemId);
                
                //Remove the item from the library
                _libraryItemRepository.Delete(itemId);
                return $"Item '{item?.Title}' removed successfully.";
            }

            //For throwing the exception inside the repository when the item to be removed does not exist
            catch (KeyNotFoundException)
            {
                throw;
            }
            
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while removing the item.", ex);
            }
        }

        //This Service method is responsible for retrieving all items in the library.
        public IEnumerable<LibraryItem> GetAllItems(User? currentUser)
        {
            // Check if the user is authorized to view items
            EnsureCanViewItems(currentUser);
            try
            {
                //Get all items from the library with their associated categories.
                return _libraryItemRepository.GetAll(li => li.Category);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving items.", ex);
            }
        }

        //This Service method is responsible for retrieving a specific item by its ID.
        public LibraryItem? GetItemById(User? currentUser, int itemId)
        {
            // Check if the user is authorized to view items
            EnsureCanViewItems(currentUser);

            //Ensure valid itemId
            EnsureValidItemId(itemId);

            try 
            {
                //Get the item by its ID from the library with its associated category.
                return _libraryItemRepository.GetById(itemId, li => li.Category);
            }
            //For throwing the exception inside the repository when the item to be retrieved does not exist
            catch (KeyNotFoundException)
            {
                throw; // Re-throw the exception
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the item by its id.", ex);
            }
        }

        //This Service method is responsible for searching items by their title.
        public IEnumerable<LibraryItem> SearchByTitle(User? currentUser, string title)
        {
            // Check if the user is authorized to view items
            EnsureCanViewItems(currentUser);

            //Check if the title is null or empty before attempting to search
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));

            try
            {
                //return the items that match the search criteria from the library with their associated categories.
                return _libraryItemRepository.SearchByTitle(title);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while searching for items.", ex);
            }
        }

        //This Service method is responsible for retrieving all available items in the library.
        public IEnumerable<LibraryItem> GetAvailableItems(User? currentUser)
        {
            // Check if the user is authorized to view items
            EnsureCanViewItems(currentUser);

            try
            {
                //return the available items from the library with their associated categories.
                return _libraryItemRepository.GetAvailableItems();
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving all avilable items in the system.", ex);
            }
        }

        //This Service method is responsible for retrieving all items in the library that belong to a specific category.
        public IEnumerable<LibraryItem> GetItemsByCategory(User? currentUser, int categoryId)
        {
            // Check if the user is authorized to view items
            EnsureCanViewItems(currentUser);

            //Ensure valid categoryId
            EnsureValidCategoryId(categoryId);

            try
            {
                //Check if the category exists before attempting to retrieve items by category
                EnsureCategoryExists(categoryId);

                //return the items that belong to the specified category from the library with their associated categories.
                return _libraryItemRepository.GetItemsByCategory(categoryId);
            }

            //For throwing the exception inside the repository when the category does not exist
            catch (KeyNotFoundException)
            {
                throw; // Re-throw the exception
            }

            catch (CategoryNotExistException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the item by categories.", ex);
            }
        }

        //This Service method is responsible for retrieving all items in the library ordered by their title.
        public IEnumerable<LibraryItem> GetItemsOrderedByTitle(User? currentUser)
        {
            // Check if the user is authorized to view items
            EnsureCanViewItems(currentUser);

            try
            {
                //return all items in the library ordered by their title with their associated categories.
                return _libraryItemRepository.GetItemsOrderedByTitle();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retriving items by titles.", ex);
            }
        }

        //This Service method is responsible for adding a new category to the library.
        public string AddCategory(User? currentUser, string name)
        {
            // Check if the user is authorized to add items
            EnsureCanAddItems(currentUser);

            //create a new category object to add to the library
            var category = new Category(name);

            try
            {
                _categoryRepository.Add(category);
                return $"Category '{category.Name}' added successfully.";
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding new category.", ex);
            }
        }

        //This service method is responsible for deleting category(important note for me hard deleting can cause problems if the item has BorrowRecord or PurchaseRecord rows connected to it.)
        public string DeleteCategory(User? currentUser, int categoryId)
        {
            //Check if currentUser is authorized to delete category
            EnsureCanDeleteItems(currentUser);

            //Ensure valid categoryId
            EnsureValidCategoryId(categoryId);

            try
            {
                var existing_category = _categoryRepository.GetById(categoryId);
                //Check if there are items belongs to this category (can not delete this category).
                var itemsUsingCategory = _libraryItemRepository.GetItemsByCategory(categoryId);
                if (itemsUsingCategory.Any())
                    throw new InvalidOperationException("Can not deleting this category because there are items using this category!");

                _categoryRepository.Delete(categoryId);
                return $"Category '{existing_category?.Name}' deleted successfully.";
            }

            //For throwing the exception inside the repository when the category does not exist
            catch (KeyNotFoundException)
            {
                throw; // Re-throw the exception
            }
            //For throwing the exception when category want to delete has items in it
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the category.", ex);
            }
        }


        public IEnumerable<Category> GetCategoriesOrderedByName(User? currentUser)
        {
            //Check if currentUser is authorized to retrive categories orderd by name
            EnsureCanViewItems(currentUser);

            try 
            {
               return _categoryRepository.GetCategoriesOrderedByName();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the categories by names.", ex);
            }
        }


        public string UpdateCategory(User? currentUser, int categoryId, string name)
        {
            // Check if the user is authorized to update items
            EnsureCanUpdateItems(currentUser);

            //Ensure valid categoryId
            EnsureValidCategoryId(categoryId);

            //create a new category object with the updated information
            var UpdatedCategory = new Category(name);

            try
            {
                //Check if the category exists before updating the category
                EnsureCategoryExists(categoryId);
                _categoryRepository.Update(categoryId, category =>
                {
                    //Update the category's properties with the new values
                    category.Rename(UpdatedCategory.Name);
                });
                return $"Category '{UpdatedCategory.Name}' updated successfully.";
            }
            //For throwing the exception inside the repository when the category does not exist
            catch (KeyNotFoundException)
            {
                throw; // Re-throw the exception
            }

            catch (CategoryNotExistException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the category.", ex);
            }

        }

        // Private helper methods for validation and authorization checks
        private void EnsureCategoryExists(int categoryId)
        {
            if (!_categoryRepository.CheckExists(categoryId))
                throw new CategoryNotExistException("Category does not exist.");
        }

        private void EnsureCanAddItems(User? currentUser)
        {
            if (!_authorizationService.CanAddItems(currentUser))
                throw new UnauthorizedAccessException("User does not have permission to add items.");
        }

        private void EnsureCanUpdateItems(User? currentUser)
        {
            if (!_authorizationService.CanUpdateItems(currentUser))
                throw new UnauthorizedAccessException("User does not have permission to update items.");
        }

        private void EnsureCanDeleteItems(User? currentUser)
        {
            if (!_authorizationService.CanDeleteItems(currentUser))
                throw new UnauthorizedAccessException("User does not have permission to delete items.");
        }

        private void EnsureCanViewItems(User? currentUser)
        {
            if (!_authorizationService.CanViewItems(currentUser))
                throw new UnauthorizedAccessException("User does not have permission to view items.");
        }

        private void EnsureValidItemId(int itemId)
        {
            if (itemId <= 0)
                throw new ArgumentException("Invalid item ID.", nameof(itemId));
        }

        private void EnsureValidCategoryId(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID.", nameof(categoryId));
        }
    }
}
