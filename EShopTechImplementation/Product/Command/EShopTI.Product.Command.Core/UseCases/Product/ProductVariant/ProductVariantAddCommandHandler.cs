using EShopTI.Product.Command.Infrastructure.EventSourcing;
using MediatR;
using ProductDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.Product;
using ProductVAriantDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.ProductVariant;

namespace EShopTI.Product.Command.Core.UseCases.Product.ProductVariant;

public class ProductVariantAddCommandHandler : IRequestHandler<ProductVariantAddCommand, ProductVAriantDomain>
{
    private readonly IEventSourcing<ProductDomain> _eventSourcing;

    public ProductVariantAddCommandHandler(IEventSourcing<ProductDomain> eventSourcing)
    {
        _eventSourcing = eventSourcing;
    }

    public async Task<ProductVAriantDomain> Handle(ProductVariantAddCommand request, CancellationToken cancellationToken)
    {
        var product = await _eventSourcing.GetByIdAsync(request.ProductId, cancellationToken);
        var variantId = Guid.NewGuid().ToString();
        product.AddVariant(variantId, request.Color, request.Size, request.Quantity, request.Price);
        await _eventSourcing.SaveAsync(product, cancellationToken);

        return product.Variants.Single(x => x.Id == variantId);
    }
}
