using Microsoft.EntityFrameworkCore;

namespace aitest3.Data;

public partial class CardtestContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        // Custom configurations can be added here if needed
        // Base configurations are handled by IdentityDbContext
    }
}
