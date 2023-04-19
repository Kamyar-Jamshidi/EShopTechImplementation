using EShopTI.Product.Query.Core.Domain.Aggregates.Category;
using EShopTI.Product.Query.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EShopTI.Product.Query.Infrastructure.Data;

public class ProductQueryDbContext : DbContext
{
    public ProductQueryDbContext(DbContextOptions<ProductQueryDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Core.Domain.Aggregates.Product.Product> Products { get; set; }
    public DbSet<Core.Domain.Aggregates.Product.ProductVariant> ProductVariants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
    }
}
