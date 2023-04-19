using EShopTI.Product.Command.Infrastructure.EventSourcing;
using MediatR;
using ProductDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.Product;

namespace EShopTI.Product.Command.Core.UseCases.Product;

public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, ProductDomain>
{
    private readonly IEventSourcing<ProductDomain> _eventSourcing;

    public ProductAddCommandHandler(IEventSourcing<ProductDomain> eventSourcing)
    {
        _eventSourcing = eventSourcing;
    }

    public async Task<ProductDomain> Handle(ProductAddCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductDomain(Guid.NewGuid().ToString(), request.Name, request.Categoryid, request.Quantity, request.Price);
        await _eventSourcing.SaveAsync(product, cancellationToken);

        return product;
    }
}
