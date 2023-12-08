using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class DemoContext:DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options):base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
