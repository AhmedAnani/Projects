using Project.src.Enums;
using Project.src.Interfaces;

namespace Project.src.Models
{
    public class Book : BookItem, IBorrowable, IBuyable
    {
        public override ItemType ItemType => ItemType.Book;

        //Parameterless constructor for EF Core
        protected Book() { }

        public Book(string title, int categoryId, string author, string description)
            : base(title, categoryId, author, description)
        {
        }


        public bool BorrowItem()
        {
            if (!IsAvailable)
                return false;

            MarkAsBorrowed();
            return true;
        }

        public void ReturnItem()
        {
            if (Status == ItemStatus.Borrowed)
                MarkAsAvailable();
        }

        public void BuyItem()
        {
            if (IsAvailable)
                MarkAsSold();
        }
        public override string DisplayInfo()
        {
            return base.DisplayInfo();
        }
    }
}