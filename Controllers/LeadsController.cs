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
    public class LeadsController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public LeadsController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> Getleads()
        {
            return await _context.leads.ToListAsync();
        }
         // GET: api/Leads/Count
        [HttpGet("Count")]
        public async Task<int> leadsCount()
        {
            var leads = await _context.leads.ToListAsync();
            return leads.Count;
        }
        
        // GET: api/Leads/5
        [HttpGet("GetLead/{id}")]
        public async Task<ActionResult<Lead>> GetLead(long id)
        {
            var lead = await _context.leads.FindAsync(id);

            if (lead == null)
            {
                return NotFound();
            }

            return lead;
        }

        // [HttpGet("{Get30DayLeads}")]
        // public async Task<ActionResult<IEnumerable<Lead>>> Get30DayLeads()
        // {
        //     var leadList = await _context.leads.ToListAsync();
        //     var customerList = await _context.customers.ToListAsync();
        //     List<Lead> lead30DayList = new List<Lead>();
        //     foreach (Lead leads in leadList)
        //     {
        //         DateTime dateNow =  DateTime.Now; 
        //         TimeSpan timeSpan = dateNow.Subtract(leads.date_of_contact_request);
        //         int result = (int) timeSpan.TotalDays;
        //         if (result <= 30)
        //         {
        //             bool isaCompany = false;
        //             foreach (Customer customers in customerList)
        //             {
        //                 if (customers.company_name == leads.company_name)
        //                 {
        //                     isaCompany = true;
        //                 }
        //             }
        //             if (!isaCompany)
        //             {
        //                 lead30DayList.Add(leads);
        //             }
        //         }
        //     }
        //     if(lead30DayList == null)
        //     {
        //         NotFound();
        //     }
        //     return lead30DayList;
        // }

        // PUT: api/Leads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLead(long id, Lead lead)
        {
            if (id != lead.id)
            {
                return BadRequest();
            }

            _context.Entry(lead).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadExists(id))
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

        // POST: api/Leads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lead>> PostLead(Lead lead)
        {
            _context.leads.Add(lead);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLead), new { id = lead.id }, lead);
        }

        // DELETE: api/Leads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead(long id)
        {
            var lead = await _context.leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            _context.leads.Remove(lead);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeadExists(long id)
        {
            return _context.leads.Any(e => e.id == id);
        }
    }
}
