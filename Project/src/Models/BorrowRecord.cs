using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public class BorrowRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("LibraryItem")]
        public int LibraryItemId { get; set; }

        public DateTime BorrowedAt { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; }

        public DateTime? ReturnedAt { get; set; }

        public bool IsReturned => ReturnedAt.HasValue;

        // Parameterless constructor for EF Core
        protected BorrowRecord() { }

        public BorrowRecord(int userId, int libraryItemId, DateTime dueDate)
        {
            UserId = userId;
            LibraryItemId = libraryItemId;
            DueDate = dueDate;
        }

        //Navigation properties

        //Mapping Relationship between BorrowRecord and LibraryItem (1:many)
        public LibraryItem? LibraryItem { get; set; }

        //Mapping Relationship between User and BorrowRecord (1:many)
        public User? User { get; set; }
    }
}
