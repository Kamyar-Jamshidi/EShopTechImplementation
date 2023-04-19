using EShopTI.Product.Command.Infrastructure.EventSourcing;
using MediatR;
using ProductDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.Product;

namespace EShopTI.Product.Command.Core.UseCases.Product;

public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand>
{
    private readonly IEventSourcing<ProductDomain> _eventSourcing;

    public ProductRemoveCommandHandler(IEventSourcing<ProductDomain> eventSourcing)
    {
        _eventSourcing = eventSourcing;
    }

    public async Task Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _eventSourcing.GetByIdAsync(request.Id, cancellationToken);
        product.Delete();
        await _eventSourcing.SaveAsync(product, cancellationToken);
    }
}
