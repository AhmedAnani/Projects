using Project.src.Enums;
using System.ComponentModel.DataAnnotations;

namespace Project.src.Models
{
    public class User
    {
        private string _name = string.Empty;
        private string _email = string.Empty;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");

                _name = value.Trim();
            }
        }

        [MaxLength(200)]
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("Invalid email format.");

                _email = value.Trim();
            }
        }

        public UserRole Role { get; set; }

        // Parameterless constructor for EF Core
        protected User() { }

        public User(string name, string email, UserRole role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        //Navigation properties

        //Mapping Relationship between User and BorrowRecord (1:many)
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

        //Mapping Relationship between User and PurchaseRecord (1:many)
        public ICollection<PurchaseRecord> PurchaseRecords { get; set; } = new List<PurchaseRecord>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}