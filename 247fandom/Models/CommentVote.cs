using System;
using Microsoft.EntityFrameworkCore;

namespace _247fandom.Models
{
    public class CommentVote: Vote
    {
        public long Id { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
