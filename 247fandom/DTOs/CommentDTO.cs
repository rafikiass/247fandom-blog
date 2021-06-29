using System;
namespace _247fandom.DTOs
{
    public class CommentDTO
    {
        public long CommentId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
