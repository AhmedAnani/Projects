using Project.src.Controller;
using Project.src.Data;
using Project.src.Enums;
using Project.src.Interfaces;
using Project.src.Models;
using Project.src.Repositories;
using Project.src.Services;

public class AppBootstrapper
{
    public LibraryManager Manager { get; }
    public LibraryItemsManager ItemsManager { get; }
    public UserManager UserManager { get; }
    public IAuthorizationService AuthService { get; }
    public User? CurrentUser { get; set; }

    public AppBootstrapper()
    {
        var context = new AppDbContext();
        SeedData.Initialize(context);

        // ── Repositories ──────────────────────────────────────
        var categoryRepo = new CategoryRepository(context);
        var itemRepo = new LibraryItemRepository(context);
        var userRepo = new UserRepository(context);
        var purchaseRepo = new PurchaseRecordRepository(context);
        var borrowRepo = new BorrowRecordRepository(context);
        var notificationRepo = new NotificationRepository(context);

        // ── Services ──────────────────────────────────────────
        AuthService = new AuthorizationService();
        var notificationService = new InAppNotificationService(notificationRepo, AuthService, userRepo);
        var buyingService = new BuyingService(AuthService, purchaseRepo, notificationService);
        var borrowingService = new BorrowingService(borrowRepo, itemRepo, AuthService, notificationService);
        var libraryItemService = new LibraryItemService(itemRepo, categoryRepo, AuthService);
        var userService = new UserService(userRepo, AuthService);

        // ── Controllers ───────────────────────────────────────
        Manager = new LibraryManager(buyingService, borrowingService);
        ItemsManager = new LibraryItemsManager(libraryItemService);
        UserManager = new UserManager(userService);

        // ── Current User ──────────────────────────────────────
        CurrentUser = userRepo.GetAll().FirstOrDefault(u => u.Role == UserRole.Admin);
    }
}