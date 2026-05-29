using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces.IRepository
{
    public interface INotificationRepository:IRepository<Notification>
    {
        IEnumerable<Notification> GetUserNotifications(int userId);
        IEnumerable<Notification> GetUnsentNotifications();
    }
}
