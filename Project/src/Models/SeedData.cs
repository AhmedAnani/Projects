using Project.src.Enums;
using Project.src.Models;

namespace Project.src.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Users.Any())
                return;

            // ================= 1. USERS =================
            var admin = new User("Admin User", "admin@test.com", UserRole.Admin);
            var ali = new User("Ali Mohamed", "ali@test.com", UserRole.User);
            var sara = new User("Sara Ahmed", "sara@test.com", UserRole.User);
            var emp = new User("Employee One", "emp1@test.com", UserRole.Employee);

            context.Users.AddRange(admin, ali, sara, emp);

            // ================= 2. CATEGORIES =================
            var programming = new Category("Programming");
            var science = new Category("Science");
            var history = new Category("History");
            var art = new Category("Art");

            context.Categories.AddRange(programming, science, history, art);
            // save changes to generate IDs for categories before adding items

            context.SaveChanges();



            // ================= 3. BOOKS =================

            var book1 = new Book("C# Basics", programming.Id, "John Doe", "Learn C# from scratch");
            var book2 = new Book("OOP Concepts", programming.Id, "Robert Martin", "Clean Object Oriented Design");
            var book3 = new Book("Physics 101", science.Id, "Albert Einstein", "Basic Physics concepts");

            context.Books.AddRange(book1, book2, book3);

            // ================= 4. EBOOKS =================
            var ebook1 = new EBook("ASP.NET Core Guide", programming.Id, "Tom", "Web development guide", "5MB");
            var ebook2 = new EBook("Data Structures", programming.Id, "CLRS", "Algorithms and DS", "8MB");

            context.EBooks.AddRange(ebook1, ebook2);

            // ================= 5. MAGAZINES =================
            var mag1 = new Magazine("Tech Monthly", programming.Id);
            var mag2 = new Magazine("Science Today", science.Id);

            context.Magazines.AddRange(mag1, mag2);
            // save changes to generate IDs for items before adding borrow/purchase records

            context.SaveChanges();

            book1.BorrowItem();
            book3.BorrowItem();

            book2.BuyItem();
            ebook1.BuyItem();

            // ================= 6. BORROW RECORDS =================
            context.BorrowRecords.AddRange(
               new BorrowRecord(ali.Id, book1.Id, DateTime.Now.AddDays(7)),
               new BorrowRecord(sara.Id, book3.Id, DateTime.Now.AddDays(10))
           );

            

            // ================= 7. PURCHASE RECORDS =================
            context.PurchaseRecords.AddRange(
                 new PurchaseRecord(ali.Id, book2.Id),
                 new PurchaseRecord(sara.Id, ebook1.Id)
             );
         
            var adminNotification = new Notification(
            admin.Id,
            "Welcome Admin 🎉",
            NotificationChannel.Email
            );
            adminNotification.MarkAsSent();

            var aliNotification = new Notification(
                ali.Id,
                "Your borrowed book is due soon",
                NotificationChannel.SMS
            );
            aliNotification.MarkAsSent();

            var saraNotification = new Notification(
                sara.Id,
                "New books added to library",
                NotificationChannel.Email
            );

            context.Notifications.AddRange(
                adminNotification,
                aliNotification,
                saraNotification
            );

            // final save to persist all seeded data
            context.SaveChanges();
        }
    }
}