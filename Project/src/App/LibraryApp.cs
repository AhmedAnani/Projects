using Project.src.App;
using Project.src.Controller;
using Project.src.Enums;

public class LibraryApp
{
    private readonly AppBootstrapper _boot;
    private readonly Dictionary<string, Action> _commands;

    public LibraryApp(AppBootstrapper appBootstrapper)
    {
        _boot = appBootstrapper ;
        _commands = new Dictionary<string, Action>
        {
            ["1"] = HandleViewAll,
            ["2"] = HandleSearch,
            ["3"] = HandleBuy,
            ["4"] = HandleBorrow,
            ["5"] = HandleReturn,
            ["7"] = HandleAddItem,
            ["8"] = HandleUpdateItem,
            ["9"] = HandleDeleteItem,
            ["10"] = HandleAddUser,
            ["11"] = HandleUpdateUser,
            ["12"] = HandleDeleteUser,
            ["13"] = HandleListUsers
        };
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
            catch (Exception ex) { ConsolePrinter.Error($"Error: {ex.Message}"); }

            if (choice != "6")
            {
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }

        } while (choice != "6");
    }

    private void HandleChoice(string choice)
    {
        if (choice == "6")
        {
            ConsolePrinter.Info("Goodbye!");
            return;
        }

        if (_commands.TryGetValue(choice, out var action))
            action();
        else
            ConsolePrinter.Warning("Invalid choice.");
    }
    // ── Items ────────────────────────────────────────────────
    private void HandleViewAll()
        => _boot.ItemsManager.ShowAllItems(_boot.CurrentUser);

    private void HandleSearch()
    {
        var id = InputHelper.GetInt("Enter item ID: ");
        _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);
     
    }

    private void HandleAddItem()
    {
        var type = InputHelper.GetEnum<ItemType>("Choose type (Book, EBook, Magazine): ");

        var title = InputHelper.GetString("Title: ");
        var catId = InputHelper.GetInt("Category ID: ");

        switch (type)
        {
            case ItemType.Book:
                {
                    var author = InputHelper.GetString("Author: ");
                    var desc = InputHelper.GetString("Description: ");
                    _boot.ItemsManager.AddBook(_boot.CurrentUser, title, catId, author, desc);
                    break;
                }

            case ItemType.EBook:
                {
                    var eAuthor = InputHelper.GetString("Author: ");
                    var eDesc = InputHelper.GetString("Description: ");
                    var fileSize = InputHelper.GetString("File Size: ");
                    _boot.ItemsManager.AddEBook(_boot.CurrentUser, title, catId, eAuthor, eDesc, fileSize);
                    break;
                }

            case ItemType.Magazine:
                {
                    _boot.ItemsManager.AddMagazine(_boot.CurrentUser, title, catId);
                    break;
                }

            default:
                ConsolePrinter.Warning("Invalid type.");
                break;
        }
    }
    private void HandleUpdateItem()
    {
        var id = InputHelper.GetInt("Enter item ID: ");
        var type = InputHelper.GetEnum<ItemType>("Choose type (Book, EBook, Magazine): ");
        var title = InputHelper.GetString("New Title: ");
        var catId = InputHelper.GetInt("New Category ID: ");

        switch (type)
        {
            case ItemType.Book:
                {
                    var author = InputHelper.GetString("Author: ");
                    var desc = InputHelper.GetString("Description: ");
                    _boot.ItemsManager.UpdateBook(_boot.CurrentUser, id, title, catId, author, desc);
                    break;
                }

            case ItemType.EBook:
                {
                    var eAuthor = InputHelper.GetString("Author: ");
                    var eDesc = InputHelper.GetString("Description: ");
                    var fileSize = InputHelper.GetString("File Size: ");
                    _boot.ItemsManager.UpdateEBook(_boot.CurrentUser, id, title, catId, eAuthor, eDesc, fileSize);
                    break;
                }

            case ItemType.Magazine:
                {
                    _boot.ItemsManager.UpdateMagazine(_boot.CurrentUser, id, title, catId);
                    break;
                }

            default:
                ConsolePrinter.Warning("Invalid type.");
                break;
        }
    }
    private void HandleDeleteItem()
    {
        var id = InputHelper.GetInt("Enter item ID: ");
        _boot.ItemsManager.RemoveItem(_boot.CurrentUser, id);      
    }

    // ── Borrow / Return / Buy ────────────────────────────────

    private void HandleBorrow()
    {
        var id = InputHelper.GetInt("Enter item ID to Borrow: ");
        var item = _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);
        if (item == null) return;
        _boot.Manager.BorrowItem(_boot.CurrentUser, item);
    }

    private void HandleReturn()
    {
        var id = InputHelper.GetInt("Enter item ID to return: ");
        var item = _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);
        if (item == null) return;
        _boot.Manager.ReturnItem(_boot.CurrentUser, item);
    }

    private void HandleBuy()
    {
        var id = InputHelper.GetInt("Enter item ID to buy: ");
        var item = _boot.ItemsManager.GetItemById(_boot.CurrentUser, id);
        if (item == null) return;
        _boot.Manager.BuyItem(_boot.CurrentUser, item);
    }

    // ── Users ────────────────────────────────────────────────

    private void HandleAddUser()
    {
        var name = InputHelper.GetString("Name: ");
        var email = InputHelper.GetString("Email: ");
        var role = InputHelper.GetEnum<UserRole>("Role (0=User, 1=Admin): ");
        _boot.UserManager.AddUser(_boot.CurrentUser, name, email, role);
    }

    private void HandleUpdateUser()
    {
        var id = InputHelper.GetInt("Enter user ID ");
        var name = InputHelper.GetString("New Name: ");
        var email = InputHelper.GetString("New Email: ");
        var role = InputHelper.GetEnum<UserRole>("Role (0=User, 1=Admin): ");
        _boot.UserManager.UpdateUser(_boot.CurrentUser, id, name, email, role);
    }

    private void HandleDeleteUser()
    {
        var id = InputHelper.GetInt("Enter user ID ");
        _boot.UserManager.DeleteUser(_boot.CurrentUser, id);   
    }

    private void HandleListUsers()
        => _boot.UserManager.ShowAllUsers(_boot.CurrentUser);
}