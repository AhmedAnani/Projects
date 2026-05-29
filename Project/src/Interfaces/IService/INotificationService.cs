using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces.IService
{
    public interface INotificationService
    {
        List<Notification> SendBorrowNotification(int userId, LibraryItem item, DateTime dueDate);
        List<Notification> SendReturnNotification(int userId, LibraryItem item, bool islate, double Fine);
        List<Notification> SendPurchaseNotification(int userId, LibraryItem item);
    }
}
