using EShopTI.Product.Common.Domain.ValueObjects;
using EShopTI.Product.Common.Domain;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product.ProductVariant;

public class ProductVariantAddCommand : IRequest<Domain.Aggregates.Product.ProductVariant>
{
    public ProductVariantAddCommand(string id, Colors color, Sizes size, string productId, int quantity, Money price)
    {
        Id = id;
        Color = color;
        Size = size;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public string Id { get; private set; }
    public Colors Color { get; private set; }
    public Sizes Size { get; private set; }
    public string ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
}
