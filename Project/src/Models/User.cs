using Project.src.Enums;
using System.ComponentModel.DataAnnotations;

namespace Project.src.Models
{
    public class User
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; } = string.Empty;
       
        [MaxLength(200)]
        public string Email { get; private set; } = string.Empty;

        public UserRole Role { get; private set; }

        //Parameterless constructor for EF Core
        protected User() { }

        public User(string name, string email, UserRole role)
        {
            UpdateProfile(name, email);
            ChangeRole(role);
        }

        //Methods that handel Encapsulations in the EF core
        public void UpdateProfile(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email format.");

            Name = name.Trim();
            Email = email.Trim();
        }

        public void ChangeRole(UserRole role)
        {
            if (!Enum.IsDefined(typeof(UserRole), role))
                throw new ArgumentException("Invalid user role.");

            Role = role;
        }

        //Navigational properties 

        // Navigation Property to mapp Relationship with BorrowRecord(1:many)
        public ICollection<BorrowRecord> BorrowRecords { get; private set; } = new List<BorrowRecord>();

        // Navigation Property to mapp Relationship with PurchaseRecords(1:many)
        public ICollection<PurchaseRecord> PurchaseRecords { get; private set; } = new List<PurchaseRecord>();
    }
}