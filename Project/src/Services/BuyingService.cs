using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.src.Models;

namespace Project.src.Services
{
    public class BuyingService
    {
        
        public void Process_Of_Buying(User user, LibraryItem item)
        {

            if(item is not IBuyable buyableItem)// Check item can be Buy or not
            {
                Console.WriteLine("This item is not available for buying.");
                return;
            }
            
            buyableItem.BuyItem();
            user.BoughtItems.Add(item);

            Console.WriteLine($"{item.Title} added to your purchases.");

        }
    }
}
