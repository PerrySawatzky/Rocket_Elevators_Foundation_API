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
    public class EmployeesController : ControllerBase
    {
        private readonly MySqlConnectionContext _context;

        public EmployeesController(MySqlConnectionContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        //Taken from elevator controller which is why this is here.
        //Keeping it here because I'm affraid to delete anything.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> ChangeEmployeeStatus(long id)
        // {
        //     var employees = await _context.employees.FindAsync(id);
        //     if (employees == null)
        //     {
        //         return NotFound();
        //     }
            
        //     if (employees.status == "online")
        //     {
        //         employees.status = "offline";
        //         _context.employees.Update(employees);
        //     }

        //     else if (employees.status == "offline")
        //     {
        //         employees.status = "online";
        //         _context.employees.Update(employees);
        //     }

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!EmployeeExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return _context.employees.Any(e => e.id == id);
        }
    }
}
