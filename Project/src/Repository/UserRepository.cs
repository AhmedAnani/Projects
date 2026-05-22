using Microsoft.EntityFrameworkCore;
using Project.src.Interfaces;
using Project.src.Models;

namespace Project.src.Repository
{
    public class UserRepo : GenericRepository<User>,IUserRepository
    {
        public UserRepo(AppDbContext context) : base(context) { }

        // Check if email exists before we add user in services
        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        // Search by email
        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}