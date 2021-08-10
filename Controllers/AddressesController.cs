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
    public class AddressesController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public AddressesController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.addresses.ToListAsync();
        }

        // GET: api/Addresses/batteriesInCities
        [HttpGet("batteriesInCities")]
        public async Task<List<int>> batteriesInCities()
        {
            var stringOfAddresses = await _context.addresses.Select(c => c.city).Distinct().ToListAsync();
            var numOfBatteries = await _context.batteries.ToListAsync();
            var arrayAPI = new List<int>();
            arrayAPI.Add(numOfBatteries.Count);
            arrayAPI.Add(stringOfAddresses.Count);
            return arrayAPI;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(long id)
        {
            var address = await _context.addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Address>>> PutAddress(Address address)
        {
            var addressUpdate = await _context.addresses.Where(a => a.id == address.id).FirstOrDefaultAsync();
            Console.WriteLine(addressUpdate);
            addressUpdate.address_type = address.address_type;
            addressUpdate.status = address.status;
            addressUpdate.entity = address.entity;
            addressUpdate.number_and_street = address.number_and_street;
            addressUpdate.suite_or_apartment = address.suite_or_apartment;
            addressUpdate.city = address.city;
            addressUpdate.postal_code = address.postal_code;
            addressUpdate.country = address.country;
            addressUpdate.notes = address.notes;
            if (addressUpdate == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddress), new { id = address.id }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(long id)
        {
            var address = await _context.addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(long id)
        {
            return _context.addresses.Any(e => e.id == id);
        }
    }
}
