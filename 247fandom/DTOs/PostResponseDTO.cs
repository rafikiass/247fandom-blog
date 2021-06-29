using System;
using System.Collections.Generic;
using _247fandom.Models;

namespace _247fandom.DTOs
{
    public class PostResponseDTO
    {
        public long PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO Author { get; set; }
        public List<CommentResponseDTO> Comments { get; set; }
    }
}
