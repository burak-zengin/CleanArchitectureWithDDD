using Domain.Products;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductItem> ProductItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
        new ProductItemConfiguration().Configure(modelBuilder.Entity<ProductItem>());

        base.OnModelCreating(modelBuilder);
    }
}
