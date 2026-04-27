using Project.src.Models;
using Project.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.src.Interfaces;
using testing.src.Models;

namespace testing.src.Services
{
    internal class LibraryManager
    {
        private readonly ILibraryRepo _libraryRepo; // Dependency on the library repository to manage library items
        private readonly AuthoService _authoService; // Dependency on the authorization service to check user permissions

        public LibraryManager(ILibraryRepo bookRepo, AuthoService authoService)
        {
            _libraryRepo = bookRepo;
            _authoService = authoService;
        }

        public void AddItem(User user, LibraryItem book)
        {
            if (_authoService.CanAdd(user))//Check if the user has permission to add items
            {
                _libraryRepo.AddItem(book);
                Console.WriteLine("Book added successfully.");
            }
            else
            {
                Console.WriteLine("You do not have permission to add books.");
            }
        }

        public void UpdateItem(User user, int id, LibraryItem updatedBook)
        {
            if (_authoService.CanUpdate(user))//Check if the user has permission to update items
            {
                _libraryRepo.UpdateItem(id, updatedBook);
                Console.WriteLine("Book updated successfully.");
            }
            else
            {
                Console.WriteLine("You do not have permission to update books.");
            }
        }

        public void DeleteItem(User user, int id)
        {
            if (_authoService.CanDelete(user))//Check if the user has permission to delete items
            {
                _libraryRepo.DeleteItem(id);
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("You do not have permission to delete books.");
            }
        }

        public List<LibraryItem> GetItems()
        {

            return _libraryRepo.GetAllItems();

        }

        public void BuyItem(User user, LibraryItem item)
        {
            if (_authoService.CanBuy(user))//Check if the user has permission to buy items
            {

                if (item is IBuyable buyableItem)//Check if the item implements IBuyable before trying to buy it
                {
                    buyableItem.BuyItem();
                }
                else
                {
                    Console.WriteLine("This item is not available for purchase.");
                }


            }
            else
            {
                Console.WriteLine("You do not have permission to buy items.");
            }
        }

        public void BorrowItem(User user, LibraryItem item)
        {
            if (_authoService.CanBorrow(user))//Check if the user has permission to borrow items
            {
                if (item is IBorrowable borrowableItem) //Check if the item implements IBorrowable before trying to borrow it
                {
                    borrowableItem.BorrowItem();
                }
                else
                {
                    Console.WriteLine("Sorry, this item is currently not available for borrowing.");
                }
            }
            else
            {
                Console.WriteLine("You do not have permission to borrow items.");
            }

        }

        public void ReturnItem(User user, LibraryItem item)
        {
            if (_authoService.CanBorrow(user))//Check if the user has permission to borrow items 
            {
                if (item is IBorrowable borrowableItem)// Check if the item implements IBorrowable before trying to return it
                {
                    borrowableItem.ReturnItem();
                }
                else
                {
                    Console.WriteLine("Sorry, this item is currently not available for returning.");
                }
            }
            else
            {
                Console.WriteLine("You do not have permission to return items.");
            }
        }


    }
}
