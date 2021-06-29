using System;
namespace _247fandom.Models
{
    public class PostVote: Vote
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
    }
}
