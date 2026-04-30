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

        var book1 = new Book(1, "C# Basics", true, "John", "Learn Programming", BookCategory.Science);
        var ebook1 = new EBook(2, "AI Future", true, "Sara", "Future of Tech", BookCategory.Science, "2MB");
        var mag1 = new Magazine(3, "Tech Today", true);

        
        manager.AddItem(admin, book1);
        manager.AddItem(admin, ebook1);
        manager.AddItem(admin, mag1);

        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Library Management System ===");
            Console.WriteLine("1. Add New Item (Admin Only)");
            Console.WriteLine("2. View All Items");
            Console.WriteLine("3. Search for Item (by ID)");
            Console.WriteLine("4. Borrow a Book");
            Console.WriteLine("5. Buy an Item (Book/EBook/Magazine)");
            Console.WriteLine("6. Return a Book");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("id of book:");
                        int BookId;
                        int.TryParse(Console.ReadLine(), out BookId);
                        Console.WriteLine("name of book:");
                        string BookName = Console.ReadLine();
                        Console.WriteLine("this book is Available or not?");
                        bool IsAvailable;
                        bool.TryParse(Console.ReadLine(), out IsAvailable);
                        Console.WriteLine("name of Author:");
                        string BookAuthor = Console.ReadLine();
                        Console.WriteLine("description :");
                        string BookDescription= Console.ReadLine();
                        Console.WriteLine("choose category of book:");
                        foreach(var category in Enum.GetValues(typeof(BookCategory)))
                        {
                            Console.WriteLine($"{(int)category}-{category}");
                        }
                        Console.Write("Enter category number or name: ");
                        string inputCategory= Console.ReadLine();
                        if (!Enum.TryParse(inputCategory, true, out BookCategory BookCategory))
                        {
                            Console.WriteLine("Invalid category");
                        }

                        var newBook = new Book(BookId, BookName, IsAvailable, BookAuthor, BookDescription, BookCategory);
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
                        Console.WriteLine("Title of book ?");
                        string chooser = Console.ReadLine()?.Trim();
                        var bookToBorrow = repo.GetAllItems().FirstOrDefault(i=>i.Title.Equals(chooser.ToLower()));
                        if (bookToBorrow == null)
                        {
                            Console.WriteLine("No books available for borrowing.");
                            
                        }
                        else manager.BorrowItem(user, bookToBorrow);
                        break;
                    case "5":
                        Console.WriteLine("Title of book ?");
                        string chooser1 = Console.ReadLine()?.Trim();
                        var itemToBuy = repo.GetAllItems().FirstOrDefault(i => i is IBuyable && i.IsAvailable && i.Title.Equals(chooser1.ToLower()));
                        if (itemToBuy != null)
                        {
                            manager.BuyItem(user, itemToBuy); 
                        }
                        else Console.WriteLine("No buyable items available.");
                        break;

                    case "6":
                        if (user.BorrowedItems.Any())
                        {
                            Console.WriteLine("Your borrowed items:");
                            foreach (var borrowItem in user.BorrowedItems)
                            {
                                Console.WriteLine($"{borrowItem.Title}");
                            }
                            Console.WriteLine("which book you want to return ?");
                            string chooser3= Console.ReadLine()?.Trim();
                            var returnbook = user.BorrowedItems.FirstOrDefault(i => i.Title.Equals(chooser3.ToLower()));
                            if (returnbook != null)
                                manager.ReturnItem(user, returnbook);
                            else
                                Console.WriteLine("You don't have a borrowed book with this title.");
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