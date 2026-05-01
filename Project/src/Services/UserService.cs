using Project.src.Models;
using Project.src.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo; // Dependency on the user repository to manage user data
        private readonly AuthoService _authoService; // Dependency on the authorization service to check user permissions
       
          
        public UserService(UserRepo userRepo, AuthoService authoService)
        {
            _userRepo = userRepo;
            _authoService = authoService;
  
        }

        public void AddUser(User user, User admin)
        {
            if (_authoService.CanManage(admin))//Check user can add user or not
            {
                _userRepo.AddUser(user);
            }
            else
            {
                Console.WriteLine("You do not have permission to manage users.");
            }
        }

        public void UpdateUser(User user, User admin, int userId)
        {
            if (_authoService.CanManage(admin))//Check user can update user details or not
            {
                _userRepo.UpdateUser(userId , user);
            }
            else
            {
                Console.WriteLine("You do not have permission to manage users.");
            }
        }
        public void DeleteUser(int userId, User admin)
        {
            if(_authoService.CanManage(admin))//Check user can Delte or not
            {
                _userRepo.DeleteUser(userId);
            }
            else
            {
                Console.WriteLine("You do not have permission to delete users.");
            }
        }
        public List<User> GetUsers(User admin)
        {
            if(!_authoService.CanManage(admin))//Check user can view all users or not
            {
                Console.WriteLine("You do not have permission to view all users.");
                return new List<User>();
            }
            return  _userRepo.Users();
           
        }
    }
}
