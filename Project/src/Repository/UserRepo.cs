using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Repository
{
    public class UserRepo
    {
        
        private List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            if (_users.Any(u => u.Id == user.Id))
            {
                throw new Exception("ID already exists.");
            }
            if (_users.Any(b => b.Email == user.Email))
            {
                throw new Exception("Email already exists.");
            }

            _users.Add(user);

        }

        public List<User> Users()
        {
            return new List<User>(_users);
        }

        public void DeleteUser(int userId)
        {
            var user = _users.Find(u => u.Id == userId);

            if (user==null)
            {
                Console.WriteLine("User not found.");
                return;

            }
            _users.Remove(user);
            Console.WriteLine($"{user.Name} Deleted Successfully.");
        }

        public void UpdateUser(int userId, User userUpdate)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == userId);

            if (existingUser == null)
            {
                Console.WriteLine("User not Found.");
                return;
            }

            try
            {
                existingUser.Name = userUpdate.Name;
                existingUser.Email = userUpdate.Email;
                existingUser.Role = userUpdate.Role;

                Console.WriteLine($"User {existingUser.Name} Updated Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Failed: {ex.Message}");
            }
        }

        public bool CheckItem(int id)
        {
            var DeleteItem = _users.Find(i => i.Id == id);
            if (DeleteItem == null)
                return false;

            return true;
        }
    }
    
}
