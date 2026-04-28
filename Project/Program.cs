using Project.src.Controller;
using Project.src.Enums;
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
        try
        { 
            var admin = new User(1, "Alice", "alice@example.com", UserRole.Admin);
        var user = new User(2, "Bob", "bob@example.com", UserRole.User);
        var admin2 = new User(1, "Alice", "alice@example.com", UserRole.Admin);
            userService.AddUser(admin, admin);
            userService.AddUser(user, admin);
            var book1 = new Book(1, "Sample Book", true, "John Doe", "Desc", BookCategory.RealWorld);
        var book2 = new Book(2, "Another Book", true, "Jane Smith", "Desc", BookCategory.Stories);
            var book4 = new Book(3, "gg Book", true, "John Doe", "Desc", BookCategory.RealWorld);
            var book3 = new Book(1, "qq Book", true, "John Doe", "Desc", BookCategory.RealWorld);
       


            Console.WriteLine("=== Add Items ===");
            manager.AddItem(admin, book1);
            manager.AddItem(user, book2);
            manager.AddItem(admin, book3);

            Console.WriteLine("\n=== All Items ===");
            Print(manager);

            Console.WriteLine("\n=== Borrow Item ===");
            manager.BorrowItem(user, book1);

            foreach (var u in userRepo.Users())
            {
                var borrowed = u.BorrowedItems.Any()
                    ? string.Join(", ", u.BorrowedItems.Select(b => b.Title))
                    : "No borrowed items";

                Console.WriteLine($"User: {u.Name}");
                Console.WriteLine($"Email: {u.Email}");
                Console.WriteLine($"Role: {u.Role}");
                Console.WriteLine($"Borrowed Items: {borrowed}");
                Console.WriteLine("----------------------");
            }
            Console.WriteLine("\n=== Update Item ===");
            var updated = new Book(99, "Updated Book", true, "New Author", "Updated", BookCategory.Science);
            manager.UpdateItem(admin, 1, updated);

            Console.WriteLine("\n=== After Update ===");
            Print(manager);

            Console.WriteLine("\n=== Delete Item ===");
            manager.DeleteItem(admin, 2);

            Console.WriteLine("\n=== Final List ===");
            Print(manager);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        
    }
    static void Print(LibraryManager manager)
    {
        foreach (var item in manager.GetItems())
        {
            item.displayInfo();
            Console.WriteLine("-------------");
        }
    }
}
