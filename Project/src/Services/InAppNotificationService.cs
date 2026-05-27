using Project.src.Enums;
using Project.src.Exceptions;
using Project.src.Interfaces;
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
        public Notification SendBorrowNotification(int userId, LibraryItem item, DateTime dueDate)
            {

            var notification = new Notification(userId, $"{NotificationChannel.InApp}:You have borrowed '{item.Title}'. It is due on {dueDate:dd/MM/yyyy}.", NotificationChannel.InApp);
            _notificationRepository.Add(notification);
            return notification;
           }

        public Notification SendPurchaseNotification(int userId, LibraryItem item)
        {

            
            var notification = new Notification(userId, $"{NotificationChannel.InApp}:You have purchased '{item.Title}'.", NotificationChannel.InApp );
            _notificationRepository.Add(notification);
            return notification;
        }

        public Notification SendReturnNotification(int userId, LibraryItem item, bool islate, double Fine)
        {
           
            string message = $"{NotificationChannel.InApp}: You have returned '{item.Title}'.";

            if (islate)
            {
                message += $" You returned late and your fine is {Fine} EGP.";
            }
            var notification = new Notification(userId, message, NotificationChannel.InApp);
            _notificationRepository.Add(notification);
            return notification;

        }
        //public void CheckIfCanSeeReports(int userId)
        //{
        //    var user = _userRepository.GetById(userId);
        //    if (!_authorizationService.CanViewReports(user))
        //    {
        //        throw new UnauthorizedAccessException("You are not authorized to see reports.");
        //    }
        //}
    }
}
