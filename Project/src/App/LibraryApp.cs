using Project.src.App;
using Project.src.Controller;
using Project.src.Enums;
using Project.src.Models;

public class LibraryApp
{
    private readonly AppBootstrapper _boot;

    public LibraryApp(AppBootstrapper appBootstrapper)
    {
        _boot = appBootstrapper;
    }

    public void Run()
    {
        if (!SelectCurrentUser())
            return;

        string choice;

        do
        {
            Console.Clear();

            if (_boot.CurrentUser == null)
            {
                ConsolePrinter.Error("No current user selected.");
                return;
            }

            MenuRenderer.ShowMenu(_boot.CurrentUser, _boot.AuthService);

            choice = Console.ReadLine() ?? "";

            try
            {
                HandleChoice(choice);
            }
            catch (Exception ex)
            {
                ConsolePrinter.Error($"Error: {ex.Message}");
            }

            if (choice != "25")
            {
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }

        } while (choice != "25");
    }

    private bool SelectCurrentUser()
    {
        while (true)
        {
            Console.Clear();
            MenuRenderer.ShowRoleMenu();

            var choice = Console.ReadLine() ?? "";

            if (choice == "4")
                return false;

            UserRole role;

            switch (choice)
            {
                case "1":
                    role = UserRole.User;
                    break;
                case "2":
                    role = UserRole.Admin;
                    break;
                case "3":
                    role = UserRole.Employee;
                    break;
                default:
                    ConsolePrinter.Warning("Invalid role choice.");
                    Console.ReadKey();
                    continue;
            }

            var name = InputHelper.GetRequiredString("Enter your name: ");
            var email = InputHelper.GetRequiredString("Enter your email: ");

            if (_boot.TrySetCurrentUser(role, name, email, out string message))
            {
                ConsolePrinter.Success(message);
                Console.ReadKey();
                return true;
            }

            ConsolePrinter.Error(message);
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }

    private void HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                HandleViewAll();
                break;
            case "2":
                HandleSearchById();
                break;
            case "3":
                HandleBuy();
                break;
            case "4":
                HandleBorrow();
                break;
            case "5":
                HandleReturn();
                break;
            case "6":
                HandleAddItem();
                break;
            case "7":
                HandleUpdateItem();
                break;
            case "8":
                HandleDeleteItem();
                break;
            case "9":
                HandleSearchById();
                break;
            case "10":
                HandleSearchByTitle();
                break;
            case "11":
                HandleAvailableItems();
                break;
            case "12":
                HandleItemsByCategory();
                break;
            case "13":
                HandleItemsOrderedByTitle();
                break;
            case "15":
                HandleAddCategory();
                break;
            case "16":
                HandleUpdateCategory();
                break;
            case "17":
                HandleDeleteCategory();
                break;
            case "18":
                HandleCategoriesOrderedByName();
                break;
            case "19":
                HandleAddUser();
                break;
            case "20":
                HandleUpdateUser();
                break;
            case "21":
                HandleDeleteUser();
                break;
            case "22":
                HandleListUsers();
                break;
            case "23":
                HandleUserById();
                break;
            case "24":
                HandleUserByEmail();
                break;
            case "25":
                ConsolePrinter.Info("Goodbye!");
                break;
            default:
                ConsolePrinter.Warning("Invalid choice.");
                break;
        }
    }

    private void HandleViewAll()
        => _boot.ItemsManager.ShowAllItems(_boot.CurrentUser);

    private void HandleSearchById()
    {
        var id = InputHelper.GetInt("Enter item ID: ");
        _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);
    }

    private void HandleSearchByTitle()
    {
        var title = InputHelper.GetRequiredString("Enter title: ");
        _boot.ItemsManager.SearchByTitle(_boot.CurrentUser, title);
    }

    private void HandleAvailableItems()
        => _boot.ItemsManager.ShowAvailableItems(_boot.CurrentUser);

    private void HandleItemsByCategory()
    {
        var categoryId = InputHelper.GetInt("Enter category ID: ");
        _boot.ItemsManager.ShowItemsByCategory(_boot.CurrentUser, categoryId);
    }

    private void HandleItemsOrderedByTitle()
        => _boot.ItemsManager.ShowItemsOrderedByTitle(_boot.CurrentUser);

    private void HandleAddItem()
    {
        var type = InputHelper.GetEnum<ItemType>("Choose type (Book, EBook, Magazine): ");
        var title = InputHelper.GetRequiredString("Title: ");
        var categoryId = InputHelper.GetInt("Category ID: ");

        switch (type)
        {
            case ItemType.Book:
                var author = InputHelper.GetRequiredString("Author: ");
                var description = InputHelper.GetString("Description: ");
                _boot.ItemsManager.AddBook(_boot.CurrentUser, title, categoryId, author, description);
                break;

            case ItemType.EBook:
                var ebookAuthor = InputHelper.GetRequiredString("Author: ");
                var ebookDescription = InputHelper.GetString("Description: ");
                var fileSize = InputHelper.GetRequiredString("File Size: ");
                _boot.ItemsManager.AddEBook(_boot.CurrentUser, title, categoryId, ebookAuthor, ebookDescription, fileSize);
                break;

            case ItemType.Magazine:
                _boot.ItemsManager.AddMagazine(_boot.CurrentUser, title, categoryId);
                break;
        }
    }

    private void HandleUpdateItem()
    {
        var id = InputHelper.GetInt("Enter item ID: ");
        var type = InputHelper.GetEnum<ItemType>("Choose type (Book, EBook, Magazine): ");
        var title = InputHelper.GetRequiredString("New Title: ");
        var categoryId = InputHelper.GetInt("New Category ID: ");

        switch (type)
        {
            case ItemType.Book:
                var author = InputHelper.GetRequiredString("Author: ");
                var description = InputHelper.GetString("Description: ");
                _boot.ItemsManager.UpdateBook(_boot.CurrentUser, id, title, categoryId, author, description);
                break;

            case ItemType.EBook:
                var ebookAuthor = InputHelper.GetRequiredString("Author: ");
                var ebookDescription = InputHelper.GetString("Description: ");
                var fileSize = InputHelper.GetRequiredString("File Size: ");
                _boot.ItemsManager.UpdateEBook(_boot.CurrentUser, id, title, categoryId, ebookAuthor, ebookDescription, fileSize);
                break;

            case ItemType.Magazine:
                _boot.ItemsManager.UpdateMagazine(_boot.CurrentUser, id, title, categoryId);
                break;
        }
    }

    private void HandleDeleteItem()
    {
        var id = InputHelper.GetInt("Enter item ID: ");
        _boot.ItemsManager.RemoveItem(_boot.CurrentUser, id);
    }

    private void HandleAddCategory()
    {
        var name = InputHelper.GetRequiredString("Category name: ");
        _boot.ItemsManager.AddCategory(_boot.CurrentUser, name);
    }

    private void HandleUpdateCategory()
    {
        var id = InputHelper.GetInt("Category ID: ");
        var name = InputHelper.GetRequiredString("New category name: ");
        _boot.ItemsManager.UpdateCategory(_boot.CurrentUser, id, name);
    }

    private void HandleDeleteCategory()
    {
        var id = InputHelper.GetInt("Category ID: ");
        _boot.ItemsManager.DeleteCategory(_boot.CurrentUser, id);
    }

    private void HandleCategoriesOrderedByName()
        => _boot.ItemsManager.ShowCategoriesOrderedByName(_boot.CurrentUser);

    private void HandleBorrow()
    {
        var id = InputHelper.GetInt("Enter item ID to borrow: ");
        var item = _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);

        if (item != null && _boot.CurrentUser != null)
            _boot.Manager.BorrowItem(_boot.CurrentUser, item);
    }

    private void HandleReturn()
    {
        var id = InputHelper.GetInt("Enter item ID to return: ");
        var item = _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);

        if (item != null && _boot.CurrentUser != null)
            _boot.Manager.ReturnItem(_boot.CurrentUser, item);
    }

    private void HandleBuy()
    {
        var id = InputHelper.GetInt("Enter item ID to buy: ");
        var item = _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);

        if (item != null && _boot.CurrentUser != null)
            _boot.Manager.BuyItem(_boot.CurrentUser, item);
    }

    private void HandleAddUser()
    {
        var name = InputHelper.GetRequiredString("Name: ");
        var email = InputHelper.GetRequiredString("Email: ");
        var role = InputHelper.GetEnum<UserRole>("Role (User, Admin, Employee): ");

        _boot.UserManager.AddUser(_boot.CurrentUser, name, email, role);
    }

    private void HandleUpdateUser()
    {
        var id = InputHelper.GetInt("Enter user ID: ");
        var name = InputHelper.GetRequiredString("New Name: ");
        var email = InputHelper.GetRequiredString("New Email: ");
        var role = InputHelper.GetEnum<UserRole>("Role (User, Admin, Employee): ");

        _boot.UserManager.UpdateUser(_boot.CurrentUser, id, name, email, role);
    }

    private void HandleDeleteUser()
    {
        var id = InputHelper.GetInt("Enter user ID: ");
        _boot.UserManager.DeleteUser(_boot.CurrentUser, id);
    }

    private void HandleListUsers()
        => _boot.UserManager.ShowAllUsers(_boot.CurrentUser);

    private void HandleUserById()
    {
        var id = InputHelper.GetInt("Enter user ID: ");
        _boot.UserManager.ShowUserById(_boot.CurrentUser, id);
    }

    private void HandleUserByEmail()
    {
        var email = InputHelper.GetRequiredString("Enter user email: ");
        _boot.UserManager.ShowUserByEmail(_boot.CurrentUser, email);
    }
}