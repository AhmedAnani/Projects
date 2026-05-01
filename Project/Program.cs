using Project.src.Controller;
using Project.src.Enums;
using Project.src.Interfaces;
using Project.src.Models;
using Project.src.Repository;
using Project.src.Services;
using System;
using System.ComponentModel.DataAnnotations;
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
        var employee=new User(3,"em","email@ishc.com",UserRole.Employee);

        userService.AddUser(admin, admin);
        userService.AddUser(user, admin);

        // Load items from file
        var loadedItems = LoadItems("items.txt");
        foreach (var item in loadedItems)
        {
            repo.AddItem(item);
        }

        var currentUser = employee; // change to user to test permissions

        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Library Management System ===");
            
            Console.WriteLine("1. View All Items");
            Console.WriteLine("2. Search for Item (by ID)");
            Console.WriteLine("3. Buy an Item");
            Console.WriteLine("4. Borrow a Book");
            Console.WriteLine("5. Return a Book");
            Console.WriteLine("6. Exit");
            if (authService.CanControl(currentUser))
            {
                Console.WriteLine("7. Add New Item");
                Console.WriteLine("8. Update  Item");
                Console.WriteLine("9. Delete Item");
            }
            if (authService.CanManage(currentUser))
            {
                Console.WriteLine("10. Add a User");
                Console.WriteLine("11. Update User info");
                Console.WriteLine("12. Delete User");
                Console.WriteLine("13. List Users");
            }
           
            Console.Write("Enter your choice: ");

            choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Print(manager);
                        break;
                    case "2":
                        Console.Write("Enter ID: ");
                        if (int.TryParse(Console.ReadLine(), out int searchId))
                        {
                            var item = repo.GetAllItems().FirstOrDefault(i => i.Id == searchId);
                            if (item != null) item.displayInfo();
                            else Console.WriteLine("Not found.");
                        }
                        break;
                    case "3":
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
                    case "6":
                        // Save before exit
                        SaveItems(repo.GetAllItems());
                        Console.WriteLine("Goodbye!");
                        break;
                    case "7":
                        if (!authService.CanControl(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.WriteLine("Select item type:");
                        foreach (var t in Enum.GetValues(typeof(TypesOfItems)))
                            Console.WriteLine($"{(int)t} - {t}");

                        string typeInput = Console.ReadLine() ?? "";
                        if (!Enum.TryParse(typeInput, true, out TypesOfItems itemType))
                        {
                            Console.WriteLine("Invalid type.");
                            break;
                        }

                        Console.Write("ID: ");
                        int.TryParse(Console.ReadLine(), out int id);
                        if (repo.CheckItem(id))
                        {
                            Console.WriteLine($"{itemType} Founded Insert Another Id.");
                            break;
                        }
                        Console.Write("Title: ");
                        string title = Console.ReadLine() ?? "";

                        Console.Write("Available (true/false): ");
                        bool.TryParse(Console.ReadLine(), out bool available);

                        switch (itemType)
                        {
                            case TypesOfItems.Magazine:
                                manager.AddItem(currentUser, new Magazine(id, title, available));
                                break;

                            case TypesOfItems.Book:
                            case TypesOfItems.EBook:
                                Console.Write("Author: ");
                                string author = Console.ReadLine() ?? "";

                                Console.Write("Description: ");
                                string desc = Console.ReadLine() ?? "";

                                Console.WriteLine("Choose Category:");
                                foreach (var c in Enum.GetValues(typeof(BookCategory)))
                                    Console.WriteLine($"{(int)c} - {c}");

                                string catInput = Console.ReadLine() ?? "";
                                Enum.TryParse(catInput, true, out BookCategory category);

                                if (itemType == TypesOfItems.Book)
                                {
                                    manager.AddItem(currentUser,
                                        new Book(id, title, available, author, desc, category));
                                }
                                else
                                {
                                    Console.Write("File Size: ");
                                    string fileSize = Console.ReadLine() ?? "";

                                    manager.AddItem(currentUser,
                                        new EBook(id, title, available, author, desc, category, fileSize));
                                }
                                LogAction(currentUser, $"Added {itemType} with ID={id}, Title={title}");
                                break;
                            default:
                                break;
                        }

                        SaveItems(repo.GetAllItems());
                        Console.WriteLine($"{itemType} added!");
                        break;
                    case "8":
                        if (!authService.CanControl(currentUser)){
                            Console.WriteLine("Access Denied.");
                            break;
                        }
                        Console.Write("Item ID: ");
                        int.TryParse(Console.ReadLine(), out int itemId);

                        var existingItem = repo.GetAllItems().FirstOrDefault(i => i.Id == itemId);

                        if (existingItem == null)
                        {
                            Console.WriteLine("Item not found.");
                            break;
                        }


                        if (existingItem is Book) itemType = TypesOfItems.Book;
                        else if (existingItem is EBook) itemType = TypesOfItems.EBook;
                        else itemType = TypesOfItems.Magazine;
                        Console.Write("Title: ");
                        string updateTitle = Console.ReadLine() ?? "";

                        Console.Write("Available (true/false): ");
                        bool.TryParse(Console.ReadLine(), out bool updateAvailable);

                        switch (itemType)
                        {
                            case TypesOfItems.Magazine:
                                manager.UpdateItem(currentUser, itemId, new Magazine(itemId, updateTitle, updateAvailable));
                                break;

                            case TypesOfItems.Book:
                            case TypesOfItems.EBook:
                                Console.Write("Author: ");
                                string updateAuthor = Console.ReadLine() ?? "";

                                Console.Write("Description: ");
                                string updateDesc = Console.ReadLine() ?? "";

                                Console.WriteLine("Choose Category:");
                                foreach (var c in Enum.GetValues(typeof(BookCategory)))
                                    Console.WriteLine($"{(int)c} - {c}");

                                string updateCatatogry = Console.ReadLine() ?? "";
                                Enum.TryParse(updateCatatogry, true, out BookCategory UpdateCategory);

                                if (itemType == TypesOfItems.Book)
                                {
                                    manager.UpdateItem(currentUser, itemId,
                                        new Book(itemId, updateTitle, updateAvailable, updateAuthor, updateDesc, UpdateCategory));
                                }
                                else
                                {
                                    Console.Write("File Size: ");
                                    string UpdateFileSize = Console.ReadLine() ?? "";

                                    manager.UpdateItem(currentUser, itemId,
                                        new EBook(itemId, updateTitle, updateAvailable, updateAuthor, updateDesc, UpdateCategory, UpdateFileSize));
                                }
                                LogAction(currentUser,$"Updated Item ID={itemId} ");
                                break;
                            default:
                                break;
                        }
                        SaveItems(repo.GetAllItems());
                        break;
                    case "9":
                        if (!authService.CanControl(currentUser)){
                            Console.WriteLine("Access Denied.");
                            break;
                        }
                        Console.Write("Item ID: ");
                        int.TryParse(Console.ReadLine(), out int DelteId);

                        manager.DeleteItem(currentUser, DelteId);
                        LogAction(currentUser,$"Deleted Item ID={DelteId}");
                        break;
                    case "10":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("User ID: ");
                        int.TryParse(Console.ReadLine(), out int uid);
                        if (userRepo.CheckItem(uid))
                        {
                            Console.WriteLine("User founded.");
                            break;
                        }
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
                        LogAction(currentUser, $"Added User ID={uid}, Name={uname}, Role={role}");
                        break;

                    case "11":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("User ID: ");
                        int.TryParse(Console.ReadLine(), out int upId);

                        if (!userRepo.CheckItem(upId))
                        {
                            Console.WriteLine("User not found.");
                            break;
                        }
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
                        LogAction(currentUser, $"Updated User ID={upId}, Name={upName}");
                        break;

                    case "12":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;
                        }

                        Console.Write("User ID: ");
                        int.TryParse(Console.ReadLine(), out int delId);

                        userService.DeleteUser(delId, currentUser);
                        LogAction(currentUser, $"Deleted User ID={delId}");
                        break;

                    case "13":
                        if (!authService.CanManage(currentUser))
                        {
                            Console.WriteLine("Access Denied.");
                            break;

                        }
                        var users = userService.GetUsers(currentUser);
                        foreach (var u in users)
                        {
                            Console.WriteLine($"{u.Id} - {u.Name} - {u.Email} - {u.Role}");
                        }
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
    static void LogAction(User user, string action)
    {
        string logLine = $"{DateTime.Now} | User: {user.Name} ({user.Role}) | {action}";
        File.AppendAllText("log.txt", logLine + Environment.NewLine);
    }
}