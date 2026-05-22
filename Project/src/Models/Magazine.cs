using Project.src.Enums;
using Project.src.Interfaces;

namespace Project.src.Models
{
    public class Magazine : LibraryItem, IBuyable
    {
        public override ItemType ItemType => ItemType.Magazine;

        //Parameterless constructor for EF Core
        protected Magazine() { }

        public Magazine(string title, int categoryId)
            : base(title, categoryId)
        {
        }

        public override string DisplayInfo()
        {
            return $"Id: {Id}\n" +
                   $"Type: {ItemType}\n" +
                   $"Title: {Title}\n" +
                   $"Category: {Category?.Name ?? "Not assigned"}\n" +
                   $"Status: {Status}";

        }

        public void BuyItem()
        {
            if (IsAvailable)
                MarkAsSold();
        }
    }
}