using System;
using _247fandom.DTOs;
using _247fandom.Models;

namespace _247fandom.Mappers.Utilities
{
    public class UserUtil
    {
        public static UserDTO BuildUserData(User user)
        {
            var userData = new UserDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return userData;
        }
    }
}
