using System.Threading.Tasks;
using _247fandom.DTOs;
using _247fandom.Mappers;
using _247fandom.Models;
using Microsoft.AspNetCore.Mvc;

namespace _247fandom.Controllers
{

    [Route("api/community")]
    [ApiController]
    public class CommunityFollowersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CommunityFollowersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("follow")]
        [HttpPost]
        public async Task<ActionResult<MembershipDTO>> Follow(MembershipDTO membership)
        {
            Community community = await _context.Community.FindAsync(membership.CommunityId);
            User user = await _context.Users.FindAsync(membership.UserId);

            //TODO simplify return
            if (community == null)
            {
                return NotFound();
            }

            if (user == null)
            {
                return NotFound();
            }

             community.Members.Add(user);
             await _context.SaveChangesAsync();

             return Ok(membership);
        }


        [Route("unfollow")]
        [HttpPost]
        public async Task<ActionResult> Unfollow(MembershipDTO membership)
        {
            Community community = await _context.Community.FindAsync(membership.CommunityId);
            User user = await _context.Users.FindAsync(membership.UserId);

            //TODO simplify return
            if (community == null || user == null)
            {
                return NotFound();
            }

            community.Members.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
