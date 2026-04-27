using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Services
{
    public class BuyingService
    {
        
        public void Process_Of_Buying(User user, Book item)
        {
            item.BuyItem();
            if (!item.IsAvailable)
            {
                user.BoughtItems.Add(item);
            }

        }
    }
}
