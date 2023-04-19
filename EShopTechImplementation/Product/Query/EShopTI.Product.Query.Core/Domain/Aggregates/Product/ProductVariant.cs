using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.Aggregates.Product;
using EShopTI.Product.Common.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace EShopTI.Product.Query.Core.Domain.Aggregates.Product;

public class ProductVariant : BaseProductVariant
{
    [JsonIgnore]
    public Product Product { get; private set; }

    public ProductVariant()
    {
    }

    public ProductVariant(string id, Colors color, Sizes size, string productId, int quantity, Money price) : base(id, color, size, productId, quantity, price)
    {
    }
}
