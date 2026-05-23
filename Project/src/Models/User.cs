using Project.src.Enums;
using Project.src.Validations;
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
            ChangeName(name);
            ChangeEmail(email);
            ChangeRole(role);
        }

        //Methods that handel Encapsulations in the EF core
        public void ChangeName(string name)
        {
            //validates name then assigns it to the property
            Name = ValidationHelper.CheckNotNullOrWhiteSpaceText(name, "Name cannot be empty.", nameof(name));
        }

        public void ChangeEmail(string email)
        {
            //validates email then assigns it to the property
            Email = ValidationHelper.CheckValidEmail(email, "Invalid email format.", nameof(email));
        }

        public void ChangeRole(UserRole role)
        {
            //validates role then assigns it to the property
            Role = ValidationHelper.CheckEnumValue(role, "Invalid user role.", nameof(role));
        }

        //Navigational properties 

        // Navigation Property to mapp Relationship with BorrowRecord(1:many)
        public ICollection<BorrowRecord> BorrowRecords { get; private set; } = new List<BorrowRecord>();

        // Navigation Property to mapp Relationship with PurchaseRecords(1:many)
        public ICollection<PurchaseRecord> PurchaseRecords { get; private set; } = new List<PurchaseRecord>();

        // Navigation Property to mapp Relationship with Notifications(1:many)
        public ICollection<Notification> Notifications { get; private set; } = new List<Notification>();
    }
}