using System;
using System.Threading.Tasks;
using _247fandom.Models;
using _247fandom.Services.Interfaces;

namespace _247fandom.Services
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext dBcontext)
        {
            _context = dBcontext;
        }

        public async Task<User> RegisterUser(User user)
        {

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
