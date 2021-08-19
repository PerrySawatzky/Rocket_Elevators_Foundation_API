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
    public class ElevatorsController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public ElevatorsController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevator()
        {
            return await _context.elevators.ToListAsync();
        }
       

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

         // GET: api/Elevators/notInOperation
        [HttpGet("notInOperation")]
        public async Task<ActionResult<IEnumerable<Elevator>>> notInOperation()
        {
            var elevatorList = await _context.elevators.ToListAsync();
            var offlineElevator = elevatorList.Where(e => e.status != "online" && e.serial_number != null).ToList();

            if (offlineElevator == null)
            {
                return NotFound();
            }

            return offlineElevator;
        }
        
        // GET: api/Elevators/elevatorBuildingCustomer
        [HttpGet("elevatorBuildingCustomer")]
        public async Task<List<int>> elevatorBuildingCustomer()
        {
            var numOfElevators = await _context.elevators.ToListAsync();
            var numOfBuildings = await _context.buildings.ToListAsync();
            var numOfCustomers = await _context.customers.ToListAsync();
            var arrayAPI = new List<int>();
            arrayAPI.Add(numOfElevators.Count);
            arrayAPI.Add(numOfBuildings.Count);
            arrayAPI.Add(numOfCustomers.Count);
            return arrayAPI;
        }
        // GET: api/Elevators/status/{id}
        [HttpGet("status/{id}")]
        public async Task<string> status(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return "Error 404; Elevator not found.";
            }
            else {
                var elevatorStatus = elevator.status;
                return elevatorStatus.ToString();
            }
        }
        // GET: api/Elevators/offline
        [HttpGet("offline")]
        public async Task<int> offline()
        {
            var elevatorList = await _context.elevators.ToListAsync();
            var offline = elevatorList.Where(e => e.status != "online").ToList();
            return offline.Count;
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeElevatorStatus(long id)
        {
            var elevators = await _context.elevators.FindAsync(id);
            if (elevators == null)
            {
                return NotFound();
            }
            
            if (elevators.status == "online")
            {
                elevators.status = "offline";
                _context.elevators.Update(elevators);
            }

            else
            {
                elevators.status = "online";
                _context.elevators.Update(elevators);
            }

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
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

        // POST: api/Elevators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        {
            _context.elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElevator), new { id = elevator.id }, elevator);
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevator(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElevatorExists(long id)
        {
            return _context.elevators.Any(e => e.id == id);
        }
    }
}
