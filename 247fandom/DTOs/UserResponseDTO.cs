using System;
using System.Collections.Generic;
using _247fandom.Models;

namespace _247fandom.DTOs
{
    public class UserResponseDTO
    {
        public UserDTO User { get; set; }
        public ICollection<PostResponseDTO> Posts { get; set; }
        public ICollection<CommunityDTO> Memberships { get; set; }
    }
}
