using Project.src.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public class BorrowRecord
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey("User")]
        public int UserId { get; private set; }

        [ForeignKey("LibraryItem")]
        public int LibraryItemId { get; private set; }

        public DateTime BorrowedAt { get; private set; } = DateTime.Now;

        public DateTime DueDate { get; private set; }

        public DateTime? ReturnedAt { get; private set; }

        [NotMapped]
        public bool IsReturned => ReturnedAt.HasValue;

        //Prameterless constructor for EF Core
        protected BorrowRecord() { }

        public BorrowRecord(int userId, int libraryItemId, DateTime dueDate)
        {
            // validate and set UserId, LibraryItemId, and DueDate
            UserId = ValidationHelper.CheckPositiveInteger(userId, "User id must be greater than zero.", nameof(userId));
            LibraryItemId = ValidationHelper.CheckPositiveInteger(libraryItemId, "Library item id must be greater than zero.", nameof(libraryItemId));

            if (dueDate <= DateTime.Now)
                throw new ArgumentException("Due date must be in the future.");

          
            DueDate = dueDate;
        }

        public void MarkReturned()
        {
            ReturnedAt = DateTime.Now;
        }

        public double CalculateFine(double finePerDay)
        {
            var endDate = ReturnedAt ?? DateTime.Now;

            var daysLate = (endDate.Date - DueDate.Date).Days;

            if (daysLate <= 0)
                return 0;

            return daysLate * finePerDay;
        }
        //Navigation properties

        // Mapping Relationship between BorrowRecord and User (1:many)
        public User? User { get; private set; }

        // Mapping Relationship between BorrowRecord and LibraryItem (1:many)
        public LibraryItem? LibraryItem { get; private set; }
    }
}