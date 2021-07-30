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
    public class CustomersController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public CustomersController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            var customer = await _context.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            if (id != customer.id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return _context.customers.Any(e => e.id == id);
        }

        
        // GET: api/Customers/user_email/{email}
        [HttpGet("user_email/{email}")] 
        public async Task<Customer> GetBuildingsOwnedByCustomer(string email)
        {

            var customerList = await _context.customers.Where(c => c.company_contact_email == email).ToListAsync();

            var customer = customerList.First();
            
            return customer;
            
//             var customers = await _context.customers.ToListAsync();
//             if (customers == null)
//             {
//                 return null;
//             }
            
//             var ourCustomer = await _context.customers.Where(c => c.company_contact_email == email).ToListAsync();
            
//             List<int> ourCustomerID = new List<int>();
//             foreach(Customer c in ourCustomer)
//             {
//                 ourCustomerID.Add(Convert.ToInt32(c.id));
//             }
            
//             return ourCustomerID;
//             var customerID = ourCustomer.id;

//             // IEnumerable<Customer> customerID;
//             // customerID = customer.Where(b => b.status != "active");
            
//             var batteries = await _context.batteries.ToListAsync();
//             var columns = await _context.columns.ToListAsync();
//             var elevators = await _context.elevators.ToListAsync();

//             IEnumerable<Battery> imperfectBatteries;
//             imperfectBatteries = batteries.Where(b => b.status != "active");

//             IEnumerable<Column> imperfectColumns;
//             imperfectColumns = columns.Where(c => c.status != "active");

//             IEnumerable<Elevator> imperfectElevators;
//             imperfectElevators = elevators.Where(e => e.status != "active");

// //Tag the bad batteries
//             List<int> imperfectColumnIDs = new List<int>();
//             foreach(Elevator e in imperfectElevators)
//             {
//                 imperfectColumnIDs.Add(Convert.ToInt32(e.id));
//             }
//             foreach(int cid in imperfectColumnIDs)
//             {
//                 imperfectColumns.Concat(columns.Where(c => c.id == cid));
//             }

//             List<int> imperfectBatteryIDs = new List<int>();
//             foreach(Column c in imperfectColumns)
//             {
//                 imperfectBatteryIDs.Add(c.battery_id);
//             }
//             foreach(int batId in imperfectBatteryIDs)
//             {
//                imperfectBatteries.Concat(batteries.Where(bat => bat.id == batId));
//             }

//             List<long> imperfectBuildingIDs = new List<long>();
//             foreach(Battery bat in imperfectBatteries)
//             {
//                 imperfectBuildingIDs.Add(long.Parse(bat.building_id));
//             }

// //Tag the bad buildings
//             // IEnumerable<Customer> imperfectBuildings = null;
//             // foreach(long id in imperfectBuildingIDs)
//             // {
//             //     imperfectBuildings = buildings.Where(b => b.id != id);
//             // }

//             return imperfectBuildings;

        }
    }
}
