using Project.src.Interfaces.IService;
using Project.src.Models;

namespace Project.src.Services
{
    public class CompositeNotificationService : INotificationService
    {
        private readonly IEnumerable<INotificationService> _services;

        public CompositeNotificationService(IEnumerable<INotificationService> services)
        {
            _services = services;
        }


        public List<Notification> SendBorrowNotification(int userId, LibraryItem item, DateTime dueDate)
    => _services.SelectMany(s => s.SendBorrowNotification(userId, item, dueDate)).ToList();

        public List<Notification> SendReturnNotification(int userId, LibraryItem item, bool islate, double Fine)
            => _services.SelectMany(s => s.SendReturnNotification(userId, item, islate, Fine)).ToList();

        public List<Notification> SendPurchaseNotification(int userId, LibraryItem item)
            => _services.SelectMany(s => s.SendPurchaseNotification(userId, item)).ToList();

    }
}