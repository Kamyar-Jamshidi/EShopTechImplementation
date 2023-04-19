using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product.ProductVariant;

public class ProductVariantRemoveCommand : IRequest
{
    public ProductVariantRemoveCommand(string id, string productId)
    {
        Id = id;
        ProductId = productId;
    }

    public string Id { get; set; }
    public string ProductId { get; set;}
}
