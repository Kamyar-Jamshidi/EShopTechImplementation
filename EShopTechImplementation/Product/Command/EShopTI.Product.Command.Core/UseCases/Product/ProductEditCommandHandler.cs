using EShopTI.Product.Command.Infrastructure.EventSourcing;
using MediatR;
using ProductDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.Product;

namespace EShopTI.Product.Command.Core.UseCases.Product;

public class ProductEditCommandHandler : IRequestHandler<ProductEditCommand, ProductDomain>
{
    private readonly IEventSourcing<ProductDomain> _eventSourcing;

    public ProductEditCommandHandler(IEventSourcing<ProductDomain> eventSourcing)
    {
        _eventSourcing = eventSourcing;
    }

    public async Task<ProductDomain> Handle(ProductEditCommand request, CancellationToken cancellationToken)
    {
        var product = await _eventSourcing.GetByIdAsync(request.Id, cancellationToken);
        product.Edit(request.Name, request.Categoryid, request.Quantity, request.Price);
        await _eventSourcing.SaveAsync(product, cancellationToken);

        return product;
    }
}
