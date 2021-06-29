using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _247fandom.Models
{
    public class User
    {
        public long UserId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        [NotMapped]
        public string Password { get; set; }

        public string AvatarUrl { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Community> Memberships { get; } = new List<Community>();
    }
}
