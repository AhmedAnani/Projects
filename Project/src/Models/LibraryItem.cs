using Project.src.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public abstract class LibraryItem
    {
        private string _title = string.Empty;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title cannot be empty.");

                _title = value.Trim();
            }
        }

        public ItemStatus Status { get; set; } = ItemStatus.Available;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [NotMapped]
        public bool IsAvailable => Status == ItemStatus.Available;

        [NotMapped]
        public abstract ItemType ItemType { get; }

        public abstract void DisplayInfo();

        // Parameterless constructor for EF Core
        protected LibraryItem() { }

        protected LibraryItem(string title, int categoryId)
        {
            Title = title;
            CategoryId = categoryId;
        }


        // Navigation property for mapping the relationship with Category (1:many)
        public Category? Category { get; set; }

        //Mapping Relationship between BorrowRecord and LibraryItem (1:many)
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

        // Mapping Relationship between PurchaseRecord and LibraryItem (1:many)
        public ICollection<PurchaseRecord> PurchaseRecords { get; set; } = new List<PurchaseRecord>();
    }
}