using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public class PurchaseRecord
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("LibraryItem")]
        public int LibraryItemId { get; set; }

        public DateTime PurchasedAt { get; set; } = DateTime.Now;

        // Parameterless constructor for EF Core
        protected PurchaseRecord() { }

        public PurchaseRecord(int userId, int libraryItemId)
        {
            UserId = userId;
            LibraryItemId = libraryItemId;
        }

        // Navigation properties

        // Mapping Relationship between PurchaseRecord and LibraryItem (1:many)
        public LibraryItem? LibraryItem { get; set; }

        //Mapping Relationship between User and PurchaseRecord (1:many)
        public User? User { get; set; }
    }
}