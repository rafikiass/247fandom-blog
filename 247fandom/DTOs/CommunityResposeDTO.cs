using System;
using System.Collections.Generic;

namespace _247fandom.DTOs
{
    public class CommunityResposeDTO
    {
       public CommunityDTO Community { get; set; }
       public List<PostResponseDTO> Posts { get; set; }
       public List<UserDTO> Members { get; set; }
    }
}
