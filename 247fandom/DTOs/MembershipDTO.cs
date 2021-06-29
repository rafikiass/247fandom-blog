using System;
using System.ComponentModel.DataAnnotations;

namespace _247fandom.DTOs
{
    public class MembershipDTO
    {
        public long UserId { get; set; }
        public long CommunityId { get; set; }

    }
}
