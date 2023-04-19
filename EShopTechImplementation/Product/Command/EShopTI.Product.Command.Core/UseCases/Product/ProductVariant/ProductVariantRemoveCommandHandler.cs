using EShopTI.Product.Command.Infrastructure.EventSourcing;
using MediatR;
using ProductDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.Product;

namespace EShopTI.Product.Command.Core.UseCases.Product.ProductVariant;

public class ProductVariantRemoveCommandHandler : IRequestHandler<ProductVariantRemoveCommand>
{
    private readonly IEventSourcing<ProductDomain> _eventSourcing;

    public ProductVariantRemoveCommandHandler(IEventSourcing<ProductDomain> eventSourcing)
    {
        _eventSourcing = eventSourcing;
    }

    public async Task Handle(ProductVariantRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _eventSourcing.GetByIdAsync(request.ProductId, cancellationToken);
        product.DeleteVariant(request.Id);
        await _eventSourcing.SaveAsync(product, cancellationToken);
    }
}
