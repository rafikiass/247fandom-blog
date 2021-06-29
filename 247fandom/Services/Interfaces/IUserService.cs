using System;
using System.Threading.Tasks;
using _247fandom.Models;

namespace _247fandom.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> RegisterUser(User userModel);
    }
}
