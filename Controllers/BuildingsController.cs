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
    public class BuildingsController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public BuildingsController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuilding()
        {
            return await _context.buildings.ToListAsync();
        }

        // GET: api/Buildings/customer_id/5
        [HttpGet("customer_email/{customer_id}")]
        public async Task<ActionResult<Building>> GetUsersBuildingWithEmail(long customer_id)
        {
            var buildings = await _context.buildings.FindAsync(customer_id);

            if (buildings == null)
            {
                return NotFound();
            }

            return buildings;
        }
        // GET: api/Buildings/customer_id/5
        [HttpGet("customer_id/{customer_id}")]
        public async Task<ActionResult<Building>> GetUsersBuilding(long customer_id)
        {
            var buildings = await _context.buildings.FindAsync(customer_id);

            if (buildings == null)
            {
                return NotFound();
            }

            return buildings;
        }
        
        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(long id, Building building)
        {
            if (id != building.id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBuilding), new { id = building.id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.id == id);
        }

        [HttpGet("ImperfectBuildings")] 
        public async Task<IEnumerable<Building>> GetImperfectBuildings()
        {

            var buildings = await _context.buildings.ToListAsync();
            if (buildings == null)
            {
                return null;
            }
            var batteries = await _context.batteries.ToListAsync();
            var columns = await _context.columns.ToListAsync();
            var elevators = await _context.elevators.ToListAsync();

            IEnumerable<Battery> imperfectBatteries;
            imperfectBatteries = batteries.Where(b => b.status != "active");

            IEnumerable<Column> imperfectColumns;
            imperfectColumns = columns.Where(c => c.status != "active");

            IEnumerable<Elevator> imperfectElevators;
            imperfectElevators = elevators.Where(e => e.status != "active");

//Tag the bad batteries
            List<int> imperfectColumnIDs = new List<int>();
            foreach(Elevator e in imperfectElevators)
            {
                imperfectColumnIDs.Add(Convert.ToInt32(e.id));
            }
            foreach(int cid in imperfectColumnIDs)
            {
                imperfectColumns.Concat(columns.Where(c => c.id == cid));
            }

            List<int> imperfectBatteryIDs = new List<int>();
            foreach(Column c in imperfectColumns)
            {
                imperfectBatteryIDs.Add(c.battery_id);
            }
            foreach(int batId in imperfectBatteryIDs)
            {
               imperfectBatteries.Concat(batteries.Where(bat => bat.id == batId));
            }

            List<long> imperfectBuildingIDs = new List<long>();
            foreach(Battery bat in imperfectBatteries)
            {
                imperfectBuildingIDs.Add(long.Parse(bat.building_id));
            }

//Tag the bad buildings
            IEnumerable<Building> imperfectBuildings = null;
            foreach(long id in imperfectBuildingIDs)
            {
                imperfectBuildings = buildings.Where(b => b.id != id);
            }

            return imperfectBuildings;

        }



    }
}
