using System;
namespace _247fandom.DTOs
{
    public class CommunityDTO
    {
        public long CommunityId { get; set; }
        public string Name { get; set; }
        public string HandleName { get; set; }
        public string About { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
