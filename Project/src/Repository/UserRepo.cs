using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Repository
{
    internal class UserRepo
    {
        private List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            if (_users.Any(u => u.Id == user.Id))
            {
                Console.WriteLine("User with this ID already exists.");
            }
            if (_users.Any(b => b.Email == user.Email))
            {
                Console.WriteLine("User with this email already exists.");
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
        }

        public void UpdateUser(int userId, User userUpdate)
        {
            var user = _users.Find(u => u.Id == userId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;

            }
            userUpdate.Id = userId;
            _users[userId] = userUpdate;


        }
    }
}
