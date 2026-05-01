using Project.src.Enums;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Services
{
    // That Class for Authorization  
    public class AuthoService
    {
        public bool CanManage(User user)
        {
            return user.Role == UserRole.Admin;
        }
        
        public bool CanControl(User user)
        {
            return  user.Role==UserRole.Employee;
        }

        public bool CanBuy(User user)
        {
            return user.Role == UserRole.User || user.Role == UserRole.Employee;
        }

        public bool CanBorrow(User user)
        {
            return user.Role == UserRole.User || user.Role == UserRole.Employee;
        }

        

    }
}
