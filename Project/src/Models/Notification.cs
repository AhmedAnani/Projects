using Project.src.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Models
{
    public class Notification
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsSent { get; set; } = false;
        public NotificationChannel Channel  { get; set; }
        [ForeignKey((nameof(User)))]
        public int UserId { get; set; }
        public User? User { get; set; }
        public bool IsDeleted { get; set; } = false;




    }
}
