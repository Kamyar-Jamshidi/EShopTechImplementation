using MediatR;

namespace EShopTI.Product.Command.Core.UseCases.Product.ProductVariant;

public class ProductVariantRemoveCommand : IRequest
{
    public ProductVariantRemoveCommand(string id, string productId)
    {
        Id = id;
        ProductId = productId;
    }

    public string Id { get; private set; }
    public string ProductId { get; private set; }
}
