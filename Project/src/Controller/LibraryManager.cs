using Project.src.Interfaces;
using Project.src.Models;
using Project.src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.src.Models;

namespace Project.src.Controller
{
    internal class LibraryManager
    {
        private readonly ILibraryRepo _libraryRepo; // Dependency on the library repository to manage library items
        private readonly AuthoService _authoService; // Dependency on the authorization service to check user permissions
        private readonly BuyingService _buyingService;
        private readonly BorrowingService _borrowingService;

        public LibraryManager(
            ILibraryRepo bookRepo,
            AuthoService authoService,
            BuyingService buyingService,
            BorrowingService borrowingService)
        {
            _libraryRepo = bookRepo;
            _authoService = authoService;
            _buyingService = buyingService;
            _borrowingService = borrowingService;
        }

        public void AddItem(User user, LibraryItem book)
        {
            if (!_authoService.CanManage(user))//Check if the user has permission to add items
            {
                Console.WriteLine("You do not have permission to add books.");
                return;
            }
            _libraryRepo.AddItem(book);
        }

        public void UpdateItem(User user, int id, LibraryItem updatedBook)
        {
            if (!_authoService.CanManage(user))//Check if the user has permission to update items
            {
                
                Console.WriteLine("You do not have permission to update books.");
                return;
            }
            _libraryRepo.UpdateItem(id, updatedBook);

        }

        public void DeleteItem(User user, int id)
        {
            if (!_authoService.CanManage(user))//Check if the user has permission to delete items
            {
                
                Console.WriteLine("You do not have permission to delete books.");
                return;
            }
            _libraryRepo.DeleteItem(id);
        }

        public List<LibraryItem> GetItems()//Return the list of all library items
        {

            return _libraryRepo.GetAllItems();

        }

        public void BuyItem(User user, Book item)
        {
            if (!_authoService.CanBuy(user))//Check if the user has permission to buy items
            {
                Console.WriteLine("You do not have permission to buy items.");
                return;
            }

            _buyingService.Process_Of_Buying(user, item);
        }

        public void BorrowItem(User user, LibraryItem item)
        {
            if (!_authoService.CanBorrow(user))//Check if the user has permission to borrow items
            {
                Console.WriteLine("You do not have permission to borrow items.");
                return;
            }
            if (item is not Book)
            {
                Console.WriteLine("Only books can be borrowed.");
                return;
            }
            _borrowingService.Process_Of_Borrow(user,(Book)item);

        }

        public void ReturnItem(User user, LibraryItem item)
        {
            if (!_authoService.CanBorrow(user))//Check if the user has permission to borrow items 
            {
                Console.WriteLine("You do not have permission to return items.");
                return;
            }
            if(item is not Book)
            {
                Console.WriteLine("Only books can be returned.");
                return;
            }
            _borrowingService.Process_Of_Return(user,(Book)item);

        }


    }
}
