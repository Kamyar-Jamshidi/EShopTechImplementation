using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.ValueObjects;
using MediatR;
using ProductVAriantDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.ProductVariant;

namespace EShopTI.Product.Command.Core.UseCases.Product.ProductVariant;

public class ProductVariantAddCommand : IRequest<ProductVAriantDomain>
{
    public ProductVariantAddCommand(Colors color, Sizes size, string productId, int quantity, Money price)
    {
        Color = color;
        Size = size;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Colors Color { get; private set; }
    public Sizes Size { get; private set; }
    public string ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; set; }
}
