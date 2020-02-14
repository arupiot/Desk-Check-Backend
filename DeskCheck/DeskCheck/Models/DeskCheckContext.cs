using Microsoft.EntityFrameworkCore;

namespace DeskCheck.Models
{
    public class DeskCheckContext : DbContext
    {
        public DeskCheckContext(DbContextOptions<DeskCheckContext> options)
            : base(options)
        {
        }

        public DbSet<Desk> Desks { get; set; }
    }
}
