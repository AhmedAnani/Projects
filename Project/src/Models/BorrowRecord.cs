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
            //Validation to ensure data integrity (Encapsulation inside the constructor)
            if (userId <= 0)
                throw new ArgumentException("User id must be positive.");

            if (libraryItemId <= 0)
                throw new ArgumentException("Library item id must be positive.");

            if (dueDate <= DateTime.Now)
                throw new ArgumentException("Due date must be in the future.");

            UserId = userId;
            LibraryItemId = libraryItemId;
            DueDate = dueDate;
        }

        public void MarkReturned()
        {
            ReturnedAt = DateTime.Now;
        }

        public double CalculateFine(double finePerDay)
        {
            if (!IsReturned)
                return 0;

            if (ReturnedAt!.Value <= DueDate)
                return 0;

            int daysLate = (ReturnedAt.Value - DueDate).Days;
            return daysLate * finePerDay;
        }

        //Navigation properties

        // Mapping Relationship between BorrowRecord and User (1:many)
        public User? User { get; private set; }

        // Mapping Relationship between BorrowRecord and LibraryItem (1:many)
        public LibraryItem? LibraryItem { get; private set; }
    }
}