using Project.src.Enums;
using Project.src.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public class Book : BookItem, IBorrowable, IBuyable
    {
        [NotMapped]
        public override ItemType ItemType => ItemType.Book;

        // Parameterless constructor for EF Core
        protected Book() { }

        public Book(string title, int categoryId, string author, string description)
            : base(title, categoryId, author, description)
        {
        }

        public bool BorrowItem()
        {
            if (!IsAvailable)
                return false;

            Status = ItemStatus.Borrowed;
            return true;
        }

        public void ReturnItem()
        {
            if (Status == ItemStatus.Borrowed)
                Status = ItemStatus.Available;
        }

        public void BuyItem()
        {
            if (IsAvailable)
                Status = ItemStatus.Sold;
        }
    }
}
