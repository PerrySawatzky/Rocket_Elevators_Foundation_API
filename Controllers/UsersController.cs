using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Foundation_API.Models;

namespace Rocket_Elevators_Foundation_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public UsersController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/nicolas.genest@codeboxx.biz
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUserEmail(string email)
        {
            var userEmail = await _context.users.Where(user => user.email == email).ToListAsync();
            
            if (userEmail == null)
            {
                return NotFound();
            }

            return Ok(userEmail);
        }

    }
}
