
using Microsoft.EntityFrameworkCore;
using Project.src.Controller;
using Project.src.Data;
using Project.src.Enums;
using Project.src.Models;
using Project.src.Repositories;
using Project.src.Services;
using System;
using System.ComponentModel.DataAnnotations;
namespace Project
{
    class Program
    {
        public static void Main(string[] args)
        {
            var context = new AppDbContext();

            var categoryRepo = new CategoryRepository(context);
            var libraryItemRepo = new LibraryItemRepository(context);
            var userRepo = new UserRepository(context);
            var purchaseRepo = new PurchaseRecordRepository(context);
            var borrowRepo = new BorrowRecordRepository(context);
            var notificationRepo = new NotificationRepository(context);

            // Services
            var authService = new AuthorizationService();
            var notificationService = new InAppNotificationService(notificationRepo);
            var buyingService = new BuyingService(authService, purchaseRepo, notificationService);
            var borrowingService = new BorrowingService(borrowRepo, libraryItemRepo, authService, notificationService);

            // Controller
            var manager = new LibraryManager(buyingService, borrowingService);

            // ── Seed ──────────────────────────────────────────────
            var category = new Category("Programming");
            categoryRepo.Add(category);

            
            var user = new User("John Doe", "john.jjjjje@example.com", UserRole.User);
            userRepo.Add(user);
            Console.WriteLine($"User Id = {user.Id}");

         
            var book = new Book("Clean Code", category.Id, "Robert Martin", "Best practices");
            libraryItemRepo.Add(book);
            Console.WriteLine($"Book Id = {book.Id}");

            // ── CRUD ──────────────────────────────────────────────
            var fetched = libraryItemRepo.GetById(book.Id);
            Console.WriteLine(fetched?.DisplayInfo());

            libraryItemRepo.Update(book.Id, b => b.Rename("Clean Code 2nd Edition")); 
            libraryItemRepo.Delete(book.Id); 

            // ── Test Services ─────────────────────────────────────
           
            var book2 = new Book("The Pragmatic Programmer", category.Id, "David Thomas", "Software craftsmanship");
            libraryItemRepo.Add(book2);
            Console.WriteLine($"Book2 Id = {book2.Id}");

            manager.BorrowItem(user, book2);
            manager.ReturnItem(user, book2);
            manager.BuyItem(user, book2);



        }
    }
}