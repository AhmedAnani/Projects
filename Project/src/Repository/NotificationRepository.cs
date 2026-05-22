using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Repository
{
    public class NotificationRepository : GenericRepository<Notification>
    {
        public NotificationRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
