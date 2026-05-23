using Project.src.Enums;
using Project.src.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public string Message { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public bool IsSent { get; private set; }

        public NotificationChannel Channel { get; private set; }

        [ForeignKey("User")]
        public int UserId { get; private set; }

        public bool IsDeleted { get; private set; }

        // Parameterless constructor for EF Core
        protected Notification() { }

        public Notification(int userId, string message, NotificationChannel channel)
        {
            
            if (userId <= 0)
                throw new ArgumentException("User id must be positive.");

            UserId = userId;
            SetMessage(message);
            ChangeChannel(channel);
            CreatedAt = DateTime.Now;
            IsSent = false;
            IsDeleted = false;
        }

        // Methods to handle encapsulation in EF Core
        public void SetMessage(string message)
        {
            // Validate message then set it
            Message = ValidationHelper.CheckNotNullOrWhiteSpaceText(message, "Message cannot be null or whitespace.", nameof(message));
        }

        public void MarkAsSent()
        {
            IsSent = true;
        }

        public void MarkAsNotSent()
        {
            IsSent = false;
        }

        public void ChangeChannel(NotificationChannel channel)
        {
            // Validate channel then set it
            Channel = ValidationHelper.CheckEnumValue(channel, "Invalid notification channel.", nameof(channel));
        }

        public void SoftDelete()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }

        //Navigation property

        //Mapping RelationShip with User(1:many)
        public User? User { get; private set; }
    }
}