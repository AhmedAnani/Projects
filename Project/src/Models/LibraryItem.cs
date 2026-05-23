using Project.src.Enums;
using Project.src.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public abstract class LibraryItem
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; private set; } = string.Empty;

        public ItemStatus Status { get; private set; } = ItemStatus.Available;

        [ForeignKey("Category")]
        public int CategoryId { get; private set; }

        [NotMapped]
        public bool IsAvailable => Status == ItemStatus.Available;

        [NotMapped]
        public abstract ItemType ItemType { get; }

        public abstract string DisplayInfo();

        //Prameterless constructor for EF Core
        protected LibraryItem() { }

        protected LibraryItem(string title, int categoryId)
        {
            Rename(title);
            ChangeCategory(categoryId);
        }

        //Methods that handel Encapsulations in the EF core
        public void Rename(string title)
        {
            //validation for title then set the title property
            Title = ValidationHelper.CheckNotNullOrWhiteSpaceText(title, "Title cannot be null or whitespace.", nameof(title));
        }

        public void ChangeCategory(int categoryId)
        {
            //validation for categoryId then set the CategoryId property
            CategoryId = ValidationHelper.CheckPositiveInteger(categoryId, "CategoryId must be greater than zero.", nameof(categoryId));
        }

        public void MarkAsAvailable()
        {
            Status = ItemStatus.Available;
        }

        public void MarkAsBorrowed()
        {
            Status = ItemStatus.Borrowed;
        }

        public void MarkAsSold()
        {
            Status = ItemStatus.Sold;
        }

        public void MarkAsRemoved()
        {
            Status = ItemStatus.Removed;
        }

        //Navigational properties

        // Navigation property for mapping the relationship with Category (1:many)
        public Category? Category { get; private set; }

        //Mapping Relationship between BorrowRecord and LibraryItem (1:many)
        public ICollection<BorrowRecord> BorrowRecords { get; private set; } = new List<BorrowRecord>();

        // Mapping Relationship between PurchaseRecord and LibraryItem (1:many)
        public ICollection<PurchaseRecord> PurchaseRecords { get; private set; } = new List<PurchaseRecord>();
    }
}