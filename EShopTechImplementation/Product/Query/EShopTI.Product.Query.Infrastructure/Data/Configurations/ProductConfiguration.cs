using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShopTI.Product.Query.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Core.Domain.Aggregates.Product.Product>
{
    public void Configure(EntityTypeBuilder<Core.Domain.Aggregates.Product.Product> builder)
    {
        builder.ToTable(nameof(Core.Domain.Aggregates.Product.Product));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();

        builder.OwnsOne(x => x.Price, x =>
        {
            x.Property(x => x.Currency);
            x.Property(x => x.Amount);
        });

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.Variants)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}
