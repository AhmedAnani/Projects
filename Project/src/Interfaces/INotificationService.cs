using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface INotificationService
    {
        Notification SendBorrowNotification(int userId, LibraryItem item, DateTime dueDate);
        public Notification SendReturnNotification(int userId, LibraryItem item, bool islate, double Fine);
        Notification SendPurchaseNotification(int userId, LibraryItem item);
    }
}
