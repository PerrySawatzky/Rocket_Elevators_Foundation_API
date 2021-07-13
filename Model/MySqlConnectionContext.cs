using Microsoft.EntityFrameworkCore;

namespace Rocket_Elevators_Foundation_API.Models
{
    public class MySqlConnectorContext : DbContext
    {
        public MySqlConnectorContext (DbContextOptions<MySqlConnectorContext> options)
            : base(options)
        {
        }

        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Elevator> elevators { get; set; }
        public DbSet<Lead> leads { get; set; }
    }
}