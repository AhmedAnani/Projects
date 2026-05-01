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
        var user = new User(2, "Yasser", "yas@ischool.com", UserRole.User);

        userService.AddUser(admin, admin);
        userService.AddUser(user, admin);

        // Load items from file
        var loadedItems = LoadItems("items.txt");
        foreach (var item in loadedItems)
        {
            repo.AddItem(item);
        }

        var currentUser = admin; // change to user to test permissions

        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Library Management System ===");
            Console.WriteLine("1. Add New Item (Admin Only)");
            Console.WriteLine("2. View All Items");
            Console.WriteLine("3. Search for Item (by ID)");
            Console.WriteLine("4. Borrow a Book");
            Console.WriteLine("5. Buy an Item");
            Console.WriteLine("6. Return a Book");
            Console.WriteLine("7. Add a User");
            Console.WriteLine("8. Update User info");
            Console.WriteLine("9. Delete User");
            Console.WriteLine("10. List Users");
            Console.WriteLine("11. Exit");
            Console.Write("Enter your choice: ");

            choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("Book ID: ");
                        int.TryParse(Console.ReadLine(), out int id);

                        Console.Write("Title: ");
                        string title = Console.ReadLine() ?? "";

                        Console.Write("Available (true/false): ");
                        bool.TryParse(Console.ReadLine(), out bool available);

                        Console.Write("Author: ");
                        string author = Console.ReadLine() ?? "";

                        Console.Write("Description: ");
                        string desc = Console.ReadLine() ?? "";

                        Console.WriteLine("Choose Category:");
                        foreach (var c in Enum.GetValues(typeof(BookCategory)))
                            Console.WriteLine($"{(int)c} - {c}");

                        string catInput = Console.ReadLine() ?? "";
                        Enum.TryParse(catInput, true, out BookCategory category);

                        var newBook = new Book(id, title, available, author, desc, category);

                        manager.AddItem(currentUser, newBook);

                        // Save after add
                        SaveItems(repo.GetAllItems());

                        Console.WriteLine("Book added!");
                        break;

                    case "2":
                        Print(manager);
                        break;

                    case "3":
                        Console.Write("Enter ID: ");
                        if (int.TryParse(Console.ReadLine(), out int searchId))
                        {
                            var item = repo.GetAllItems().FirstOrDefault(i => i.Id == searchId);
                            if (item != null) item.displayInfo();
                            else Console.WriteLine("Not found.");
                        }
                        break;

                    case "4":
                        if (!authService.CanBorrow(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("Enter Title: ");
                        string borrowTitle = Console.ReadLine()?.ToLower() ?? "";

                        var book = repo.GetAllItems()
                            .FirstOrDefault(i => i.Title.ToLower() == borrowTitle);

                        if (book != null)
                            manager.BorrowItem(currentUser, book);
                        else
                            Console.WriteLine("Not found.");
                        break;

                    case "5":
                        if (!authService.CanBuy(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("Enter Title: ");
                        string buyTitle = Console.ReadLine()?.ToLower() ?? "";

                        var itemToBuy = repo.GetAllItems()
                            .FirstOrDefault(i =>
                                i is IBuyable &&
                                i.IsAvailable &&
                                i.Title.ToLower() == buyTitle);

                        if (itemToBuy != null)
                            manager.BuyItem(currentUser, itemToBuy);
                        else
                            Console.WriteLine("Not available.");
                        break;

                    case "6":
                        if (!authService.CanBorrow(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        if (!currentUser.BorrowedItems.Any())
                        {
                            Console.WriteLine("No borrowed books.");
                            break;
                        }

                        foreach (var b in currentUser.BorrowedItems)
                            Console.WriteLine(b.Title);

                        Console.Write("Return which: ");
                        string returnTitle = Console.ReadLine()?.ToLower() ?? "";

                        var returnItem = currentUser.BorrowedItems
                            .FirstOrDefault(i => i.Title.ToLower() == returnTitle);

                        if (returnItem != null)
                            manager.ReturnItem(currentUser, returnItem);
                        else
                            Console.WriteLine("Not found.");
                        break;

                    case "7":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("User ID: ");
                        int.TryParse(Console.ReadLine(), out int uid);

                        Console.Write("Name: ");
                        string uname = Console.ReadLine() ?? "";

                        Console.Write("Email: ");
                        string uemail = Console.ReadLine() ?? "";

                        Console.WriteLine("Role:");
                        foreach (var r in Enum.GetValues(typeof(UserRole)))
                            Console.WriteLine($"{(int)r} - {r}");

                        string roleInput = Console.ReadLine() ?? "";
                        Enum.TryParse(roleInput, true, out UserRole role);

                        userService.AddUser(new User(uid, uname, uemail, role), currentUser);
                        break;

                    case "8":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("User ID: ");
                        int.TryParse(Console.ReadLine(), out int upId);

                        Console.Write("Name: ");
                        string upName = Console.ReadLine() ?? "";

                        Console.Write("Email: ");
                        string upEmail = Console.ReadLine() ?? "";

                        Console.WriteLine("Role:");
                        foreach (var r in Enum.GetValues(typeof(UserRole)))
                            Console.WriteLine($"{(int)r} - {r}");

                        string upRoleInput = Console.ReadLine() ?? "";
                        Enum.TryParse(upRoleInput, true, out UserRole upRole);

                        userService.UpdateUser(new User(upId, upName, upEmail, upRole), currentUser, upId);
                        break;

                    case "9":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("User ID: ");
                        int.TryParse(Console.ReadLine(), out int delId);

                        userService.DeleteUser(delId, currentUser);
                        break;

                    case "10":
                        var users = userService.GetUsers(currentUser);
                        foreach (var u in users)
                        {
                            Console.WriteLine($"{u.Id} - {u.Name} - {u.Email} - {u.Role}");
                        }
                        break;

                    case "11":
                        // Save before exit
                        SaveItems(repo.GetAllItems());
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (choice != "11")
            {
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }

        } while (choice != "11");
    }

    static void Print(LibraryManager manager)
    {
        var items = manager.GetItems();
        if (!items.Any()) Console.WriteLine("Empty.");

        foreach (var item in items)
        {
            item.displayInfo();
            Console.WriteLine("------------");
        }
    }

    // LOAD FROM FILE
    public static List<LibraryItem> LoadItems(string path)
    {
        var items = new List<LibraryItem>();

        if (!File.Exists(path))
            return items;

        foreach (var line in File.ReadAllLines(path))
        {
            var p = line.Split('|');

            try
            {
                if (p[0] == "Book")
                    items.Add(new Book(int.Parse(p[1]), p[2], bool.Parse(p[3]), p[4], p[5], Enum.Parse<BookCategory>(p[6])));

                else if (p[0] == "EBook")
                    items.Add(new EBook(int.Parse(p[1]), p[2], bool.Parse(p[3]), p[4], p[5], Enum.Parse<BookCategory>(p[6]), p[7]));

                else if (p[0] == "Magazine")
                    items.Add(new Magazine(int.Parse(p[1]), p[2], bool.Parse(p[3])));
            }
            catch
            {
                Console.WriteLine($"Bad line: {line}");
            }
        }

        return items;
    }

    // SAVE TO FILE
    public static void SaveItems(List<LibraryItem> items)
    {
        var lines = new List<string>();

        foreach (var i in items)
        {
            if (i is Book b)
                lines.Add($"Book|{b.Id}|{b.Title}|{b.IsAvailable}|{b.Author}|{b.Description}|{b.Category}");

            else if (i is EBook e)
                lines.Add($"EBook|{e.Id}|{e.Title}|{e.IsAvailable}|{e.Author}|{e.Description}|{e.Category}|{e.FileSize}");

            else if (i is Magazine m)
                lines.Add($"Magazine|{m.Id}|{m.Title}|{m.IsAvailable}");
        }

        File.WriteAllLines("items.txt", lines);
    }
}