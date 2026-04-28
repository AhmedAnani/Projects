using Project.src.Enums;
using Project.src.Models;
using Project.src.Repository;
using Project.src.Services;
using System;
using testing.src.Models;
using testing.src.Services;
class Program
{
    public static void Main(string[] args)
    {
       
            var repo = new LibraryRepo();
        var userRepo = new UserRepo();
        var authService = new AuthoService();
        var manager = new LibraryManager(repo, authService);
        var userService = new UserService(userRepo, authService);
        try
        { 
            var admin = new User(1, "Alice", "alice@example.com", UserRole.Admin);
        var user = new User(2, "Bob", "bob@example.com", UserRole.User);
        var admin2 = new User(1, "Alice", "alice@example.com", UserRole.Admin);
            userService.AddUser(new User(1, "Alice", "alice@example.com", UserRole.Admin), admin);
            userService.AddUser(new User(3, "Charlie", "charlie@example.com", UserRole.User), admin);
            userService.AddUser(new User(3, "Charlie", "charlie@example.com", UserRole.User), user);
        var book1 = new Book(1, "Sample Book", true, "John Doe", "Desc", BookCategory.RealWorld);
        var book2 = new Book(2, "Another Book", true, "Jane Smith", "Desc", BookCategory.Stories);
        var book3 = new Book(1, "qq Book", true, "John Doe", "Desc", BookCategory.RealWorld);
        var book4 = new Book(3, "gg Book", true, "John Doe", "Desc", BookCategory.RealWorld);


            Console.WriteLine("=== Add Items ===");
            manager.AddItem(admin, book1);
            manager.AddItem(user, book2);
            manager.AddItem(admin, book3);

            Console.WriteLine("\n=== All Items ===");
            Print(manager);

            Console.WriteLine("\n=== Borrow Item ===");
            manager.BorrowItem(user, book1);

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

        //    try
        //    {
        //        var repo = new LibraryRepo();
        //        var authService = new AuthoService();
        //        var libraryManager = new LibraryManager(repo, authService);

        //        List<User> users = new List<User> { new User(1, "Alice", "alice@example.com", UserRole.Admin),
        //                                        new User(2, "Bob", "bob@example.com", UserRole.User)
        //                                         };

        //        List<LibraryItem> lists = new List<LibraryItem>(){new Book(1, "Sample Book", true, "John Doe", "A sample book description", BookCategory.RealWorld),
        //                                                   new Book(2, "Another Book", true, "Jane Smith", "Another book description", BookCategory.Stories),
        //                                                   new EBook(3, "Sample eBook", true, "John Doe", "A sample eBook description", BookCategory.RealWorld, "10MB"),
        //                                                   new EBook(4, "Another eBook", true, "Jane Smith", "Another eBook description", BookCategory.Stories, "15MB"),
        //                                                   new Magazine(5, "Sample Magazine", true)};





        //        Console.WriteLine("========++++=======++++======");

        //        try
        //         {
        //            foreach (var item in lists)
        //            {
        //                foreach (var user in users)
        //                {

        //                    libraryManager.AddItem(user, item);

        //                    Console.WriteLine("=========================");
        //                }
        //            }

        //        }
        //        catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        //        var allitems=libraryManager.GetItems();
        //      foreach (var item in allitems)
        //        {
        //           item.displayInfo();
        //            Console.WriteLine("//////////////////////");
        //        }
        //        Console.WriteLine("=========================");
        //        var updateItem = new Book(6, "Updated Book", true, "Updated Author", "Updated description", BookCategory.Science);
        //        foreach (var item in lists)
        //        {
        //            foreach (var user in users)
        //            {
        //                Console.WriteLine("========++++=======++++======");

        //                try
        //                {
        //                    libraryManager.AddItem(user, item);
        //                    libraryManager.BuyItem(user, item);
        //                    libraryManager.UpdateItem(user, item.Id, updateItem);
        //                    libraryManager.BorrowItem(user, item);
        //                    libraryManager.ReturnItem(user, item);
        //                }
        //                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        //            }
        //        }
        //        libraryManager.DeleteItem(users[1], lists[1].Id);
        //        libraryManager.DeleteItem(users[0], lists[1].Id);

        //        var nallitems = libraryManager.GetItems();
        //        foreach (var item in nallitems)
        //        {
        //            item.displayInfo();
        //            Console.WriteLine("//////////////////////");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());

        //    }
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
