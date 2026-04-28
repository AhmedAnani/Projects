using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing.src.Models
{
    internal class Magazine : LibraryItem, IBuyable
    {
        public Magazine(int id, string title, bool isAvailable) : base(id, title, isAvailable)
        {
        }
        public override void displayInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Title: {Title}");
            if (IsAvailable)
                Console.WriteLine("Is Available");
            else
                Console.WriteLine("Is Not Available");
        }

        public void BuyItem()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine($"You have bought the magazine: {Title}");
            }
            else
            {
                Console.WriteLine("Sorry, this magazine is currently not available for buying.");
            }
        }
    }
}
