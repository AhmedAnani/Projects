using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Models
{
    public class AppDbContext:DbContext
    {
        private const string ConnectionString =
            @"Server=.;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configuration for LibraryItem
            modelBuilder.Entity<LibraryItem>()
                .HasDiscriminator<string>("LibraryItemType")
                .HasValue<Book>("Book")
                .HasValue<EBook>("EBook")
                .HasValue<Magazine>("Magazine");


            // Configuration for User  --> Ensure email is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Notification>()
                .HasQueryFilter(n => !n.IsDeleted);
        }

        // DbSets
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<EBook> EBooks { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<PurchaseRecord> PurchaseRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
