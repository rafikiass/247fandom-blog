using System;
using System.ComponentModel.DataAnnotations;

namespace _247fandom.Models
{
    public class Vote
    {
        public enum EmotionTypesEnum
        { 
            Like,
            Dislike,
            LOL,
            Angry,
            OMG,
            Heart,
            HeartBroken
        }
        [Required]
        public EmotionTypesEnum VoteType { get; set; }

        public long UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
