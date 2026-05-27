using Project.src.Controller;
using Project.src.Data;
using Project.src.Interfaces;
using Project.src.Models;
using Project.src.Repositories;
using Project.src.Services;

namespace Project.src.App
{
    public class AppBootstrapper
    {
        public LibraryManager Manager { get; }
        public IAuthorizationService AuthService { get; }
        public LibraryItemRepository ItemRepo { get; }
        public UserRepository UserRepo { get; }
        public User CurrentUser { get; set; }

        public AppBootstrapper()
        {
            var context = new AppDbContext();

            // ── Seed ──────────────────────────────────────────────
            SeedData.Initialize(context);

            // ── Repositories ──────────────────────────────────────
            var categoryRepo = new CategoryRepository(context);
            ItemRepo = new LibraryItemRepository(context);
            UserRepo = new UserRepository(context);
            var purchaseRepo = new PurchaseRecordRepository(context);
            var borrowRepo = new BorrowRecordRepository(context);
            var notificationRepo = new NotificationRepository(context);

            // ── Services ──────────────────────────────────────────
            AuthService = new AuthorizationService();
            var notificationService = new InAppNotificationService(notificationRepo, AuthService, UserRepo);
            var buyingService = new BuyingService(AuthService, purchaseRepo, notificationService);
            var borrowingService = new BorrowingService(borrowRepo, ItemRepo, AuthService, notificationService);

            // ── Controller ────────────────────────────────────────
            Manager = new LibraryManager(buyingService, borrowingService);

            // ── Set default current user (first regular User) ─────
            CurrentUser = UserRepo.GetAll().First(u => u.Role == Enums.UserRole.User);
        }
    }
}