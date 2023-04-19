using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product;

public class ProductRemoveCommand : IRequest
{
    public ProductRemoveCommand(string id)
    {
        Id = id;
    }

    public string Id { get; private set; }
}
