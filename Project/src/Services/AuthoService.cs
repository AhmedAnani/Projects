using Project.src.Enums;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Services
{
    internal class AuthoService
    {
        public bool CanUpdate(User user)
        {
            return user.Role == UserRole.Admin;
        }
        public bool CanAdd(User user)
        {
            return user.Role == UserRole.Admin;
        }

        public bool CanDelete(User user)
        {
            return user.Role == UserRole.Admin;
        }   
        
        public bool CanBuy(User user)
        {
            return user.Role == UserRole.User ;
        }

        public bool CanBorrow(User user)
        {
            return user.Role == UserRole.User;
        }

       
    }
}
