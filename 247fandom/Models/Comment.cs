using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _247fandom.Models
{
    public class Comment
    {
 
        public long CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long PostId { get; set; }
        public Post Post { get; set; }

        public virtual ICollection<CommentVote> Votes { get; set; }

    }


}
