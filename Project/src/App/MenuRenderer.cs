using Project.src.Enums;
using Project.src.Interfaces.IService;
using Project.src.Models;

namespace Project.src.App
{
    public static class MenuRenderer
    {
        public static void ShowRoleMenu()
        {
            PrintBox("Choose Current User Role", new[]
            {
                "1. User",
                "2. Admin",
                "3. Employee",
                "4. Exit"
            });

            Console.Write("\nEnter your choice: ");
        }

        public static void ShowMenu(User currentUser, IAuthorizationService auth)
        {
            var options = new List<string>
            {
                $"Current User: {currentUser.Name} ({currentUser.Role})",
                ""
            };

            if (auth.CanViewItems(currentUser))
            {
                options.Add("1. View All Items");
                options.Add("2. Get Item By ID");
                options.Add("10. Search Item By Title");
                options.Add("11. Get Available Items");
                options.Add("12. Get Items By Category");
                options.Add("13. Get Items Ordered By Title");
                options.Add("18. Get Categories Ordered By Name");
            }

            if (auth.CanBuy(currentUser))
                options.Add("3. Buy an Item");

            if (auth.CanBorrow(currentUser))
            {
                options.Add("4. Borrow a Book");
                options.Add("5. Return a Book");
            }

            if (auth.CanAddItems(currentUser))
            {
                options.Add("6. Add New Item");
                options.Add("15. Add Category");
            }

            if (auth.CanUpdateItems(currentUser))
            {
                options.Add("7. Update Item");
                options.Add("16. Update Category");
            }

            if (auth.CanDeleteItems(currentUser))
            {
                options.Add("8. Delete Item");
                options.Add("17. Delete Category");
            }

            if (auth.CanManageUsers(currentUser))
            {
                options.Add("19. Add User");
                options.Add("20. Update User");
                options.Add("21. Delete User");
                options.Add("22. Get All Users");
                options.Add("23. Get User By ID");
                options.Add("24. Get User By Email");
            }
            if (auth.CanViewReports(currentUser))
            {
                options.Add("26. View Borrow Records");
                options.Add("27. View Purchase Records");
            }
           
            options.Add("25. Exit");

            PrintBox("Library Management System", options);
            Console.Write("\nEnter your choice: ");
        }

        private static void PrintBox(string title, IEnumerable<string> lines)
        {
            var content = lines.ToList();
            int width = Math.Max(title.Length, content.Any() ? content.Max(x => x.Length) : 0) + 4;

            Console.WriteLine("+" + new string('-', width) + "+");
            Console.WriteLine("| " + title.PadRight(width - 2) + " |");
            Console.WriteLine("+" + new string('-', width) + "+");

            foreach (var line in content)
                Console.WriteLine("| " + line.PadRight(width - 2) + " |");

            Console.WriteLine("+" + new string('-', width) + "+");
        }
    }
}