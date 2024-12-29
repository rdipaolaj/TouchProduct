using Microsoft.EntityFrameworkCore;
using product.entities;

namespace product.data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Definir DbSets
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("productsch");
        base.OnModelCreating(modelBuilder);
    }
}