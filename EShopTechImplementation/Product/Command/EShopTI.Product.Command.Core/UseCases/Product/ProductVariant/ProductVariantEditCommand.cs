using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.ValueObjects;
using MediatR;
using ProductVAriantDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.ProductVariant;

namespace EShopTI.Product.Command.Core.UseCases.Product.ProductVariant;

public class ProductVariantEditCommand : IRequest<ProductVAriantDomain>
{
    public ProductVariantEditCommand(string id, Colors color, Sizes size, string productId, int quantity, Money price)
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
    public Money Price { get; set; }
}