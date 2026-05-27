using Project.src.Enums;
using Project.src.Interfaces;
using Project.src.Models;

namespace Project.src.App
{
    public class LibraryApp
    {
        private readonly AppBootstrapper _boot;

        public LibraryApp()
        {
            _boot = new AppBootstrapper();
        }

        public void Run()
        {
            string choice;
            do
            {
                Console.Clear();
                MenuRenderer.ShowMenu(_boot.CurrentUser, _boot.AuthService);
                choice = Console.ReadLine() ?? "";

                try { HandleChoice(choice); }
                catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }

                if (choice != "6")
                {
                    Console.WriteLine("\nPress any key...");
                    Console.ReadKey();
                }

            } while (choice != "6");
        }

        // ── Router ───────────────────────────────────────────────

        private void HandleChoice(string choice)
        {
            switch (choice)
            {
                case "1": HandleViewAll(); break;
                case "2": HandleSearch(); break;
                case "3": HandleBuy(); break;
                case "4": HandleBorrow(); break;
                case "5": HandleReturn(); break;
                case "6": Console.WriteLine("Goodbye!"); break;
                case "7": HandleAddItem(); break;
                case "8": HandleUpdateItem(); break;
                case "9": HandleDeleteItem(); break;
                case "10": HandleAddUser(); break;
                case "11": HandleUpdateUser(); break;
                case "12": HandleDeleteUser(); break;
                case "13": HandleListUsers(); break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }

        // ════════════════════════════════════════════════════════
        //  IMPLEMENTED
        // ════════════════════════════════════════════════════════

        private void HandleBorrow()
        {
          

            Console.Write("Enter book title to borrow: ");
            string title = Console.ReadLine()?.ToLower() ?? "";

        }

        private void HandleReturn()
        {
            
        }

        private void HandleBuy()
        {
           
        }

        // ════════════════════════════════════════════════════════
        //  TODO 
        // ════════════════════════════════════════════════════════

        private void HandleViewAll()
        {
            
        }

        private void HandleSearch()
        {
            // TODO: ask for ID, find item, call item.DisplayInfo()
            Console.WriteLine("TODO: Search by ID");
        }

        private void HandleAddItem()
        {
          
            Console.WriteLine("TODO: Add New Item");
        }

        private void HandleUpdateItem()
        {
           
            Console.WriteLine("TODO: Update Item");
        }

        private void HandleDeleteItem()
        {
           
            Console.WriteLine("TODO: Delete Item");
        }

        private void HandleAddUser()
        {

            Console.WriteLine("TODO: Add User");
        }

        private void HandleUpdateUser()
        {
            

            // TODO: read user ID + new fields, call _boot.UserRepo.Update(...)
            Console.WriteLine("TODO: Update User");
        }

        private void HandleDeleteUser()
        {
           

            // TODO: read user ID, call _boot.UserRepo.Delete(...)
            Console.WriteLine("TODO: Delete User");
        }

        private void HandleListUsers()
        {
          
            // TODO: fetch all users and display them
            // var users = _boot.UserRepo.GetAll();
            // foreach (var u in users) Console.WriteLine($"{u.Id} - {u.Name} - {u.Email} - {u.Role}");
            Console.WriteLine("TODO: List Users");
        }
    }
}