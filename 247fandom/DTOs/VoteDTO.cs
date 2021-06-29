using System;
namespace _247fandom.DTOs
{
    public class VoteDTO
    {
        public long VoteId { get; set; }
        public int VoteType { get; set; }
        public UserDTO User { get; set; }
    }
}
