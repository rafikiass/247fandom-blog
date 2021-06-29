using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _247fandom.Models
{
    public class Community
    {
        public long CommunityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HandleName { get; set; }
        public string About { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<User> Members { get;} = new List<User>();
    }
}
