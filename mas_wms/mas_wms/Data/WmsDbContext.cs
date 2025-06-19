using mas_wms.Model;
using Microsoft.EntityFrameworkCore;

namespace mas_wms.Data;


public class WmsDbContext : DbContext
{
    public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options) { }

    public DbSet<FinishedGood> FinishedGoods { get; set; }
    public DbSet<Lot> Lots { get; set; }

    // Minimal configuration - most is handled by annotations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Only specify what can't be done with annotations
        modelBuilder.Entity<Item>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<FinishedGood>("FinishedGood");
            
        // EF Core automatically creates foreign key from navigation property
        // No explicit foreign key configuration needed!
    }
}


