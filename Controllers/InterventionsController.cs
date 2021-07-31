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
    public class InterventionsController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public InterventionsController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetIntervention()
        {
            return await _context.interventions.ToListAsync();
        }

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }
        // GET: api/interventions/PendingInterventions
        [HttpGet("PendingInterventions")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetTheseInterventions()
        {
            var interventionList = await _context.interventions.ToListAsync();
            var pendingInterventions = interventionList.Where(e => e.status == "Pending" && e.start_of_intervention == null).ToList();
            return pendingInterventions;
        }
        // PUT: api/interventions/{id}/InProgress
        [HttpPut("{id}/InProgress")]
        public async Task<ActionResult<Intervention>> ChangeInterventionStatus(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            else
            {
                intervention.status = "InProgress";
                intervention.start_of_intervention = DateTime.Now;
            }
                _context.interventions.Update(intervention);
                await _context.SaveChangesAsync();
            return Content("Intervention " + intervention.id + " is now " + intervention.status + " starting at " + intervention.start_of_intervention);
        }
        // PUT: api/interventions/{id}/Completed
        [HttpPut("{id}/Completed")]
        public async Task<ActionResult<Intervention>> CompletedInterventionStatus(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            else
            {
                intervention.status = "Completed";
                intervention.end_of_intervention = DateTime.Now;
            }
                _context.interventions.Update(intervention);
                await _context.SaveChangesAsync();
            return Content("Intervention " + intervention.id + " is now " + intervention.status + ". Good job! Intervention was marked completed at " + intervention.end_of_intervention + ".");
        }
        // PUT: api/Interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
         [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(long id, Intervention intervention)
        {
            if (id != intervention.id)
            {
                return BadRequest();
            }

            _context.Entry(intervention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
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

        // POST: api/Interventions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            intervention.created_at = DateTime.Now;
            intervention.updated_at = DateTime.Now;
            intervention.status = "InProgress";
            intervention.result = "Incomplete";
            intervention.employee_id = null;
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIntervention), new { id = intervention.id }, intervention);
        }

        // DELETE: api/Interventions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            _context.interventions.Remove(intervention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterventionExists(long id)
        {
            return _context.interventions.Any(e => e.id == id);
        }



    }
}
