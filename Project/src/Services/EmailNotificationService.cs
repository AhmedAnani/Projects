using Microsoft.VisualBasic;
using Project.src.Enums;
using Project.src.Exceptions;
using Project.src.Interfaces;
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
        public Notification SendBorrowNotification(int userId, LibraryItem item, DateTime dueDate)
        {
        
            var user = _userRepository.GetById(userId);

            var notification = new Notification(userId, $"{NotificationChannel.Email}:You have borrowed '{item.Title}'. It is due on {dueDate:MMMM dd, yyyy}.", NotificationChannel.Email);
            _notificationRepository.Add(notification);
            return notification;
        }

        public Notification SendPurchaseNotification(int userId, LibraryItem item)
        {
           

            var notification = new Notification(userId, $"{NotificationChannel.Email}:You have purchased '{item.Title}'.", NotificationChannel.Email);
            _notificationRepository.Add(notification);
            return notification;
        }

        public Notification SendReturnNotification(int userId, LibraryItem item, bool islate, double Fine)
        {
        
            string message = $"{NotificationChannel.Email}: You have returned '{item.Title}'.";

            if (islate)
            {
                message += $" You returned late and your fine is {Fine} EGP.";
            }
            var notification = new Notification(userId, message, NotificationChannel.Email);
            _notificationRepository.Add(notification);
            return notification;

        }
     
    }
}
