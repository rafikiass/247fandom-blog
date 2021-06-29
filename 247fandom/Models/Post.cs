using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _247fandom.Models
{
    public class Post
    {
    
        public long PostId { get; set; }
        [Required]
        public string  Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public long UserId { get; set; }
        public User User { get; set; }

        [Required]
        public long CommunityId { get; set; }
        public Community Community { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<PostVote> Votes { get; set; }

    }
}
