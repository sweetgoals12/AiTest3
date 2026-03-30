using Microsoft.EntityFrameworkCore;

namespace aitest3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NameEntry> NameEntries { get; set; }
    }
}
