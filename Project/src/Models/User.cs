using Project.src.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Models
{
    public class User
    {
        private int _id;

        private string _name;

        private string _email;

        private UserRole _role;

        public int Id { get => _id;
            set{ 
                if(value <= 0) throw new Exception($"ID must be a positive integer not{value}");

                _id = value;
            } }

        public string Name { get => _name;  
            set {
                if(string.IsNullOrEmpty(value)) throw new Exception("Name cannot be null or empty");

                _name = value; 
            } }

        public string Email
        {
            get=> _email;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains("@"))
                {
                    throw new Exception("Invalid email format");
                }
                _email = value;
            }
        }
        
         

        

        public UserRole Role
        {
            get => _role;
            set
            {
                if (!Enum.IsDefined(typeof(UserRole), value))
                {
                    string roles = string.Join(", ", Enum.GetNames<UserRole>());
                    throw new Exception($"Invalid role: {value}. Allowed values are: {roles}");
                }
                _role = value;
            }
        }
        

        public User(int id, string name, string email,UserRole role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }
        public List<LibraryItem> BorrowedItems { get; set; } = new List<LibraryItem>();
        public List<LibraryItem> BoughtItems { get; set; } = new List<LibraryItem>();

    }
}
