using System;
using System.Collections.Generic;

namespace _247fandom.DTOs
{
    public class VoteResponseDTO
    {
        public dynamic Votable { get; set; }
        public List<VoteDTO> Votes { get; set; }
    }
}
