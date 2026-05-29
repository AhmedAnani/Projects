using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces.IService
{
    public interface IBuyingServise
    {
        public Result ProcessBuy(User user, LibraryItem item);
    }
}
