using Project.src.Interfaces;
using Project.src.Models;

namespace Project.src.App
{
    public static class MenuRenderer
    {
        public static void ShowMenu(User currentUser, IAuthorizationService auth)
        {
          
            Console.WriteLine("=== Library Management System ===");
            Console.WriteLine("1. View All Items");

            if (auth.CanBuy(currentUser))
            {
                Console.WriteLine("2. Search for Item (by ID)");
                Console.WriteLine("3. Buy an Item");
                Console.WriteLine("4. Borrow a Book");
                Console.WriteLine("5. Return a Book");
            }

            Console.WriteLine("6. Exit");

            if (auth.CanAddItems(currentUser)) Console.WriteLine("7. Add New Item");       
             if(auth.CanUpdateItems(currentUser))  Console.WriteLine("8. Update Item");
             if(auth.CanDeleteItems(currentUser))  Console.WriteLine("9. Delete Item");
            

            if (auth.CanManageUsers(currentUser))
            {
              
                Console.WriteLine("10. Add a User");
                Console.WriteLine("11. Update User info");
                Console.WriteLine("12. Delete User");
                Console.WriteLine("13. List Users");
            }

            Console.Write("\nEnter your choice: ");
        }

        public static void PrintItems(IEnumerable<LibraryItem> items)
        {
            if (!items.Any()) { Console.WriteLine("No items found."); return; }
            foreach (var item in items)
            {
                item.DisplayInfo();
                Console.WriteLine("------------");
            }
        }
    }
}