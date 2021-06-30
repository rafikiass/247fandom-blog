using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _247fandom.Models;
using _247fandom.Mappers;
using _247fandom.DTOs;

namespace _247fandom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CommunityDataMapper _communityDataMapper;


        public CommunitiesController(ApplicationDbContext context,
            CommunityDataMapper communityDataMapper)
        {
            _context = context;
            _communityDataMapper = communityDataMapper;
        }

        // GET: api/Communities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Community>>> GetCommunity()
        {
            return await _context.Community.ToListAsync();
        }

        // GET: api/Communities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommunityResposeDTO>> GetCommunity(long id)
        {
            //var community = await _context.Community.FindAsync(id);
            var community = await _context
                .Community
                .Include(c => c.Posts).ThenInclude(p => p.User)
                .Include(c => c.Posts).ThenInclude(p => p.Comments).ThenInclude(cc => cc.User)
                .Include(c => c.Members)
                .FirstOrDefaultAsync(e => e.CommunityId == id);

            if (community == null)
            {
                return NotFound();
            }

            return _communityDataMapper.MapCommunityRespose(community);
        }

        // PUT: api/Communities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunity(long id, Community community)
        {
            if (id != community.CommunityId)
            {
                return BadRequest();
            }

            _context.Entry(community).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Communities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Community>> PostCommunity(Community community)
        {
            _context.Community.Add(community);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommunity", new { id = community.CommunityId }, community);
        }

        // DELETE: api/Communities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(long id)
        {
            var community = await _context.Community.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            _context.Community.Remove(community);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommunityExists(long id)
        {
            return _context.Community.Any(e => e.CommunityId == id);
        }
    }
}
