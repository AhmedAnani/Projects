using Project.src.Controller;
using Project.src.Data;
using Project.src.Enums;
using Project.src.Interfaces;
using Project.src.Models;
using Project.src.Repositories;
using Project.src.Services;

public class AppBootstrapper
{
    private readonly IUserRepository _userRepository;

    public LibraryManager Manager { get; }
    public LibraryItemsManager ItemsManager { get; }
    public UserManager UserManager { get; }
    public IAuthorizationService AuthService { get; }
    public User? CurrentUser { get; private set; }

    public AppBootstrapper()
    {
        var context = new AppDbContext();
        SeedData.Initialize(context);

        // ------------------ Repositores ----------------------------------------------------
        var categoryRepo = new CategoryRepository(context);
        var itemRepo = new LibraryItemRepository(context);
        var userRepo = new UserRepository(context);
        var purchaseRepo = new PurchaseRecordRepository(context);
        var borrowRepo = new BorrowRecordRepository(context);
        var notificationRepo = new NotificationRepository(context);

        _userRepository = userRepo;

        // ------------------ Services ----------------------------------------------------
        AuthService = new AuthorizationService();
        var notificationService = new InAppNotificationService(notificationRepo, AuthService, userRepo);
        var buyingService = new BuyingService(AuthService, purchaseRepo, notificationService);
        var borrowingService = new BorrowingService(borrowRepo, itemRepo, AuthService, notificationService);
        var libraryItemService = new LibraryItemService(itemRepo, categoryRepo, AuthService);
        var userService = new UserService(userRepo, AuthService);

        // ------------------ Managers ----------------------------------------------------
        Manager = new LibraryManager(buyingService, borrowingService);
        ItemsManager = new LibraryItemsManager(libraryItemService);
        UserManager = new UserManager(userService);
    }

    // ------------------ Current User ----------------------------------------------------
    public bool TrySetCurrentUser(UserRole role, string name, string email, out string message)
    {
        message = string.Empty;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        {
            message = "Name and email are required.";
            return false;
        }

        var user = _userRepository.GetByEmail(email);

        if (user == null)
        {
            message = "No user found with this email.";
            return false;
        }

        if (!string.Equals(user.Name, name.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            message = "Name does not match this email.";
            return false;
        }

        if (user.Role != role)
        {
            message = "Selected role does not match this user.";
            return false;
        }

        CurrentUser = user;
        message = $"Welcome, {user.Name}.";
        return true;
    }
}