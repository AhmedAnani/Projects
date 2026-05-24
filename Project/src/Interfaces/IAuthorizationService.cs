using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface IAuthorizationService
    {
        bool CanManageUsers(User? user);
        bool CanViewReports(User? user);
        bool CanAddItems(User? user);
        bool CanUpdateItems(User? user);
        bool CanDeleteItems(User? user);
        bool CanViewItems(User? user);
        bool CanBorrow(User? user);
        bool CanBuy(User? user);
    }
}
