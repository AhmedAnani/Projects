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

        public override void DisplayInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Type: {ItemType}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Category: {Category?.Name ?? "Not assigned"}");
            Console.WriteLine($"Status: {Status}");
        }

        public void BuyItem()
        {
            if (IsAvailable)
                MarkAsSold();
        }
    }
}