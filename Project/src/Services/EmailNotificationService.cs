using Microsoft.VisualBasic;
using Project.src.Enums;
using Project.src.Exceptions;
using Project.src.Interfaces.IRepository;
using Project.src.Interfaces.IService;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Project.src.Services
{
    public class EmailNotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserRepository _userRepository;

        public EmailNotificationService(INotificationRepository notificationRepository, IAuthorizationService authorizationService, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _authorizationService = authorizationService;
            _userRepository = userRepository;
        }
        public List<Notification> SendBorrowNotification(int userId, LibraryItem item, DateTime dueDate)
        {
            var notification = new Notification(userId, $"Email Notification: You have borrowed '{item.Title}'. It is due on {dueDate:MMMM dd, yyyy}.", NotificationChannel.Email);
            _notificationRepository.Add(notification);
            return new List<Notification> { notification };
        }

        public List<Notification> SendPurchaseNotification(int userId, LibraryItem item)
        {
            var notification = new Notification(userId, $"Email Notification: You have purchased '{item.Title}'.", NotificationChannel.Email);
            _notificationRepository.Add(notification);
            return new List<Notification> { notification };
        }

        public List<Notification> SendReturnNotification(int userId, LibraryItem item, bool islate, double Fine)
        {
            string message = $"Email Notification: You have returned '{item.Title}'.";
            if (islate) message += $" You returned late and your fine is {Fine} EGP.";
            var notification = new Notification(userId, message, NotificationChannel.Email);
            _notificationRepository.Add(notification);
            return new List<Notification> { notification };
        }

    }
}
