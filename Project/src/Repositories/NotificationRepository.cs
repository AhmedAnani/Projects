using Microsoft.EntityFrameworkCore;
using Project.src.Interfaces.IRepository;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context):base(context)
        {
            
        }

        // This method is used to get all notifications for a specific user
        public IEnumerable<Notification> GetUserNotifications(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));

            return _context.Notifications
                .Include(n => n.User) // Include related User entity
                .AsNoTracking()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
                
        }

        // This method is used to get all unsent notifications
        public IEnumerable<Notification> GetUnsentNotifications()
        {
            return _context.Notifications
                .Include(n => n.User)
                .AsNoTracking()
                .Where(n => !n.IsSent)
                .OrderBy(n => n.CreatedAt)
                .ToList();
        }
    }
}
