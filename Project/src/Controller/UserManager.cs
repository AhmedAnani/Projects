using Project.src.Enums;
using Project.src.Exceptions;
using Project.src.Interfaces.IService;
using Project.src.Models;
using System;
using System.Collections.Generic;

namespace Project.src.Controller
{
    public class UserManager
    {
        private readonly IUserService _userService;

        //Make injection for IUserService inside constructor
        public UserManager(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // Method That manage AddUser
        public void AddUser(User? currentUser, string name, string email, UserRole role)
        {
            try
            {
                string message = _userService.AddUser(currentUser, name, email, role);
                ConsolePrinter.Success(message);
            }
            
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        // Method That manage UpdateUser
        public void UpdateUser(User? currentUser,int userId, string name, string email, UserRole role)
        {
            try
            {
                string message = _userService.UpdateUser(currentUser, userId, name, email, role);
                ConsolePrinter.Success(message);
            }
            
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        // Method That manage DeletedUser
        public void DeleteUser(User? currentUser, int userId)
        {
            try
            {
                string message = _userService.DeleteUser(currentUser, userId);
                ConsolePrinter.Success(message);
            }
            
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        // Method That Return All users of the Library
        public void ShowAllUsers(User? currentUser)
        {
            try
            {
                var users = _userService.GetAllUsers(currentUser);
                UserConsolePrinter.Users(users);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        // Method That Return User by its Id
        public void ShowUserById(User? currentUser, int userId)
        {
            try 
            {
                var existingUser = _userService.GetUserById(currentUser, userId);
                if (existingUser == null)
                {
                    ConsolePrinter.Warning("User not found.");
                    return;
                }
               
                UserConsolePrinter.UserInfo(existingUser);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        // Method That Return User by its email
        public void ShowUserByEmail(User? currentUser, string email)
        {
            try
            {
                var existingUser = _userService.GetUserByEmail(currentUser, email);
                if (existingUser == null)
                {
                    ConsolePrinter.Warning("User not found.");
                    return;
                }

                UserConsolePrinter.UserInfo(existingUser);
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        //Method for handling Exceptions
        private static void PrintError(Exception ex)
        {
            if (ex is UnauthorizedAccessException ||
                ex is ArgumentException ||
                ex is KeyNotFoundException ||
                ex is EmailExistsException ||
                ex is InvalidOperationException)
            {
                ConsolePrinter.Error(ex.Message);
                return;
            }

            ConsolePrinter.Error($"Unexpected error: {ex.Message}");
        }
    }
}
