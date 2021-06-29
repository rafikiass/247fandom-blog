using System;
using static _247fandom.Models.Vote;

namespace _247fandom.DTOs
{
    public class VoteRequestDTO
    {
      public long VotableId { get; set; }
      public String Votable { get; set; }
      public EmotionTypesEnum VoteType { get; set; }
      public long UserId { get; set; }
    }
}
