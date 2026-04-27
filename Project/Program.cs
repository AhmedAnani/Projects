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
        try
        {
            var repo = new LibraryRepo();
            var authService = new AuthoService();
            var libraryManager = new LibraryManager(repo, authService);

            List<User> users = new List<User> { new User(1, "Alice", "alice@example.com", UserRole.Admin),
                                            new User(2, "Bob", "bob@example.com", UserRole.User)
                                             };

            List<LibraryItem> lists = new List<LibraryItem>(){new Book(1, "Sample Book", true, "John Doe", "A sample book description", BookCategory.RealWorld),
                                                       new Book(2, "Another Book", true, "Jane Smith", "Another book description", BookCategory.Stories),
                                                       new EBook(3, "Sample eBook", true, "John Doe", "A sample eBook description", BookCategory.RealWorld, "10MB"),
                                                       new EBook(4, "Another eBook", true, "Jane Smith", "Another eBook description", BookCategory.Stories, "15MB"),
                                                       new Magazine(5, "Sample Magazine", true)};





            Console.WriteLine("========++++=======++++======");
            
            try
             {
                foreach (var item in lists)
                {
                    foreach (var user in users)
                    {
                        
                        libraryManager.AddItem(user, item);
                       
                        Console.WriteLine("=========================");
                    }
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            var allitems=libraryManager.GetItems();
          foreach (var item in allitems)
            {
               item.displayInfo();
                Console.WriteLine("//////////////////////");
            }
            Console.WriteLine("=========================");
            var updateItem = new Book(6, "Updated Book", true, "Updated Author", "Updated description", BookCategory.Science);
            foreach (var item in lists)
            {
                foreach (var user in users)
                {
                    Console.WriteLine("========++++=======++++======");

                    try
                    {
                        libraryManager.AddItem(user, item);
                        libraryManager.BuyItem(user, item);
                        libraryManager.UpdateItem(user, item.Id, updateItem);
                        libraryManager.BorrowItem(user, item);
                        libraryManager.ReturnItem(user, item);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                }
            }
            libraryManager.DeleteItem(users[1], lists[1].Id);
            libraryManager.DeleteItem(users[0], lists[1].Id);

            var nallitems = libraryManager.GetItems();
            foreach (var item in nallitems)
            {
                item.displayInfo();
                Console.WriteLine("//////////////////////");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());

        }
    }
}
