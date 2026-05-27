using Project.src.Enums;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface IUserService
    {
        string AddUser(User? currentUser, string name, string email, UserRole role);
        string UpdateUser(User? currentUser, int userId, string name, string email, UserRole role);
        string DeleteUser(User? currentUser, int userId);
        IEnumerable<User> GetAllUsers(User? currentUser);
        User? GetUserByEmail(User? currentUser, string email);

        User? GetUserById(User? currentUser, int userId);
    }
}
