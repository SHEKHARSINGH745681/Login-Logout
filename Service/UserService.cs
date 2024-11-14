using EmployeeAuthentication.Data;
using EmployeeAuthentication.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeAuthentication.Service
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public bool Register(string username, string password)
        {
            // Check if the user already exists in the database
            if (_context.Users.Any(u => u.Username == username))
            {
                return false;  // User already exists
            }

            var user = new User { Username = username };
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;  // Invalid username
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}