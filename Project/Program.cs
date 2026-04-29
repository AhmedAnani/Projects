using Project.src.Controller;
using Project.src.Enums;
using Project.src.Interfaces;
using Project.src.Models;
using Project.src.Repository;
using Project.src.Services;
using System;
using testing.src.Models;

class Program
{
    public static void Main(string[] args)
    {

        var repo = new LibraryRepo();
        var userRepo = new UserRepo();
        var buyingService = new BuyingService();
        var borrowingService = new BorrowingService();
        var authService = new AuthoService();
        var manager = new LibraryManager(repo, authService, buyingService, borrowingService);
        var userService = new UserService(userRepo, authService);

        
        var admin = new User(1, "Aya Hassan", "aya@ischool.com", UserRole.Admin);
        var user = new User(2, "Bob", "bob@example.com", UserRole.User);
        userService.AddUser(admin, admin);
        userService.AddUser(user, admin);

        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Library Management System ===");
            Console.WriteLine("1. Add New Item (Admin Only)");
            Console.WriteLine("2. View All Items");
            Console.WriteLine("3. Search for Item (by ID)");
            Console.WriteLine("4. Borrow a Book");
            Console.WriteLine("5. Buy an Item (EBook/Magazine)");
            Console.WriteLine("6. Return a Book");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                       
                        var newBook = new Book(101, "Clean Code", true, "Robert Martin", "Software Principles", BookCategory.Science);
                        manager.AddItem(admin, newBook);
                        Console.WriteLine("Book added successfully!");
                        break;

                    case "2":
                        Console.WriteLine("\n--- All Items ---");
                        Print(manager);
                        break;

                    case "3":
                        Console.Write("Enter Item ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var item = repo.GetAllItems().FirstOrDefault(i => i.Id == id);
                            if (item != null) item.displayInfo();
                            else Console.WriteLine("Item not found.");
                        }
                        break;

                    case "4":
                        var bookToBorrow = repo.GetAllItems().FirstOrDefault(i => i is Book && i.IsAvailable);
                        if (bookToBorrow != null)
                        {
                            manager.BorrowItem(user, (Book)bookToBorrow);
                        }
                        else Console.WriteLine("No books available for borrowing.");
                        break;

                    case "5":
                        var itemToBuy = repo.GetAllItems().FirstOrDefault(i => i is IBuyable && i.IsAvailable);
                        if (itemToBuy != null)
                        {
                            manager.BuyItem(user, itemToBuy); 
                        }
                        else Console.WriteLine("No buyable items available.");
                        break;

                    case "6":
                        if (user.BorrowedItems.Any())
                        {
                            manager.ReturnItem(user, user.BorrowedItems[0]);
                        }
                        else Console.WriteLine("You have no borrowed items.");
                        break;

                    case "7":
                        Console.WriteLine("Exiting... Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (choice != "7")
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

        } while (choice != "7"); 
    }

    static void Print(LibraryManager manager)
    {
        var items = manager.GetItems();
        if (!items.Any()) Console.WriteLine("Library is empty.");
        foreach (var item in items)
        {
            item.displayInfo();
            Console.WriteLine("-------------");
        }
    }
}