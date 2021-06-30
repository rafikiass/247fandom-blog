using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _247fandom.Models;
using _247fandom.Services.Interfaces;
using _247fandom.Mappers;
using _247fandom.DTOs;

namespace _247fandom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly UserDataMapper _userDataMapper;

        public UsersController(ApplicationDbContext context,
            IUserService userService,
            UserDataMapper userDataMapper)
        {
            _context = context;
            _userService = userService;
            _userDataMapper = userDataMapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            List<User> users =  await _context.Users.ToListAsync();
            return _userDataMapper.MapUserResponse(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser(long id)
        {
            var user = await _context.Users
                .Include(u => u.Posts).ThenInclude(p  => p.Comments).ThenInclude(c => c.User)
                .Include(u => u.Memberships)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return _userDataMapper.MapUserResponse(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
        
            if (id != user.UserId)
            {
                return BadRequest();
            }

            try
            {
                var dbUser = await _context.Users.FindAsync(id);
                if (dbUser != null)
                {
                    _userDataMapper.mapModelToState(user, dbUser);
                    _context.Entry(dbUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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



        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            User savedUser = await _userService.RegisterUser(user);
            return CreatedAtAction("GetUser", new { id = savedUser.UserId }, _userDataMapper.MapUserResponse(savedUser));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

 
    }
}
