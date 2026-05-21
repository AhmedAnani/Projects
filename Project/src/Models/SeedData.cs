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
           
            var book1 = new Book("C# Basics", programming.Id, "John Doe", "Learn C# from scratch") { Status = ItemStatus.Available };
            var book2 = new Book("OOP Concepts", programming.Id, "Robert Martin", "Clean Object Oriented Design") { Status = ItemStatus.Available };
            var book3 = new Book("Physics 101", science.Id, "Albert Einstein", "Basic Physics concepts") { Status = ItemStatus.Available };

            context.Books.AddRange(book1, book2, book3);

            // ================= 4. EBOOKS =================
            var ebook1 = new EBook("ASP.NET Core Guide", programming.Id, "Tom", "Web development guide", "5MB") { Status = ItemStatus.Available };
            var ebook2 = new EBook("Data Structures", programming.Id, "CLRS", "Algorithms and DS", "8MB") { Status = ItemStatus.Available };

            context.EBooks.AddRange(ebook1, ebook2);

            // ================= 5. MAGAZINES =================
            var mag1 = new Magazine("Tech Monthly", programming.Id) { Status = ItemStatus.Available };
            var mag2 = new Magazine("Science Today", science.Id) { Status = ItemStatus.Available };

            context.Magazines.AddRange(mag1, mag2);
            // save changes to generate IDs for items before adding borrow/purchase records

            context.SaveChanges();

            // ================= 6. BORROW RECORDS =================
            context.BorrowRecords.AddRange(
                new BorrowRecord(ali.Id, book1.Id, DateTime.Now.AddDays(7)) { BorrowedAt = DateTime.Now.AddDays(-2) },
                new BorrowRecord(sara.Id, book3.Id, DateTime.Now.AddDays(10)) { BorrowedAt = DateTime.Now }
            );

            // ================= 7. PURCHASE RECORDS =================
            context.PurchaseRecords.AddRange(
                new PurchaseRecord(ali.Id, book2.Id) { PurchasedAt = DateTime.Now.AddDays(-5) },
                new PurchaseRecord(sara.Id, ebook1.Id) { PurchasedAt = DateTime.Now }
            );

            // ================= 8. NOTIFICATIONS =================
            context.Notifications.AddRange(
                new Notification { UserId = admin.Id, Message = "Welcome Admin 🎉", Channel = NotificationChannel.Email, CreatedAt = DateTime.Now.AddDays(-10), IsSent = true },
                new Notification { UserId = ali.Id, Message = "Your borrowed book is due soon", Channel = NotificationChannel.SMS, CreatedAt = DateTime.Now.AddDays(-1), IsSent = true },
                new Notification { UserId = sara.Id, Message = "New books added to library", Channel = NotificationChannel.Email, CreatedAt = DateTime.Now, IsSent = false }
            );

            // final save to persist all seeded data
            context.SaveChanges();
        }
    }
}