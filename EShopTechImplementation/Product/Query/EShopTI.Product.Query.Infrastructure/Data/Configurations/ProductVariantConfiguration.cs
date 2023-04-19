using EShopTI.Product.Query.Core.Domain.Aggregates.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShopTI.Product.Query.Infrastructure.Data.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable(nameof(ProductVariant));
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Color).IsRequired();
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();

        builder.OwnsOne(x => x.Price, x =>
        {
            x.Property(x => x.Currency);
            x.Property(x => x.Amount);
        });

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Variants)
            .HasForeignKey(x => x.ProductId);
    }
}
