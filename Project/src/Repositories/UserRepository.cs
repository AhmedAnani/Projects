using Microsoft.EntityFrameworkCore;
using Project.src.Interfaces;
using Project.src.Models;

namespace Project.src.Repositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        // Check if email exists before we add user in services
        public bool EmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            var normalizedEmail = email.Trim().ToLower();

            return _context.Users
                .AsNoTracking()
                .Any(u => u.Email.ToLower() == normalizedEmail);
        }

        // Search by email
        public User? GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            var normalizedEmail = email.Trim().ToLower();

            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);
        }
    }
}