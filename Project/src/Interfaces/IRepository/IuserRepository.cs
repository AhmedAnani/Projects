using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces.IRepository
{
    public interface IUserRepository: IRepository<User>
    {
        public bool EmailExists(string email);
        public User? GetByEmail(string email);
    }
}
