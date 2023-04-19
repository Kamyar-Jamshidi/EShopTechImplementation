using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.Aggregates.Product;
using EShopTI.Product.Common.Domain.ValueObjects;

namespace EShopTI.Product.Command.Core.Domain.Aggregates.Product;

public class ProductVariant : BaseProductVariant
{
    public ProductVariant(string id, Colors color, Sizes size, string productId, int quantity, Money price) : base(id, color, size, productId, quantity, price)
    {
    }
}