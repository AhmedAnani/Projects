using Project.src.Enums;
using Project.src.Exceptions;
using Project.src.Interfaces.IRepository;
using Project.src.Interfaces.IService;
using Project.src.Models;
using Project.src.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Services
{
     public class InAppNotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserRepository _userRepository;

        public InAppNotificationService(INotificationRepository notificationRepository, IAuthorizationService authorizationService, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _authorizationService = authorizationService;
            _userRepository = userRepository;
        }
        public List<Notification> SendBorrowNotification(int userId, LibraryItem item, DateTime dueDate)
        {
            var notification = new Notification(userId, $"InApp Notification: You have borrowed '{item.Title}'. It is due on {dueDate:dd/MM/yyyy}.", NotificationChannel.InApp);
            _notificationRepository.Add(notification);
            return new List<Notification> { notification };
        }

        public List<Notification> SendPurchaseNotification(int userId, LibraryItem item)
        {
            var notification = new Notification(userId, $"InApp Notification: You have purchased '{item.Title}'.", NotificationChannel.InApp);
            _notificationRepository.Add(notification);
            return new List<Notification> { notification };
        }

        public List<Notification> SendReturnNotification(int userId, LibraryItem item, bool islate, double Fine)
        {
            string message = $"InApp Notification: You have returned '{item.Title}'.";
            if (islate) message += $" You returned late and your fine is {Fine} EGP.";
            var notification = new Notification(userId, message, NotificationChannel.InApp);
            _notificationRepository.Add(notification);
            return new List<Notification> { notification };
        }

    }
}
