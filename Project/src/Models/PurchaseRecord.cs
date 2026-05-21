using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public class PurchaseRecord
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey("User")]
        public int UserId { get; private set; }

        [ForeignKey("LibraryItem")]
        public int LibraryItemId { get; private set; }

        public DateTime PurchasedAt { get; private set; } = DateTime.Now;

        //Parameterless constructor for EF Core
        protected PurchaseRecord() { }

        public PurchaseRecord(int userId, int libraryItemId)
        {
            //Validation for the properties to ensure data integrity(Encapsulation inside the constructor)
            if (userId <= 0)
                throw new ArgumentException("User id must be positive.");

            if (libraryItemId <= 0)
                throw new ArgumentException("Library item id must be positive.");

            UserId = userId;
            LibraryItemId = libraryItemId;
        }

        //Navigation Properties

        //Mapping Relationship between PurchaseRecord and User (1:many)
        public User? User { get; private set; }

        //Mapping Relationship between PurchaseRecord and LibraryItem (1:many)
        public LibraryItem? LibraryItem { get; private set; }
    }
}