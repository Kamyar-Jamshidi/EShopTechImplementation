using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product.ProductVariant;

public class ProductVariantAddCommandHandler : IRequestHandler<ProductVariantAddCommand, Domain.Aggregates.Product.ProductVariant>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductVariantAddCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Aggregates.Product.ProductVariant> Handle(ProductVariantAddCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(request.ProductId, cancellationToken);
        product.AddVariant(request.Id, request.Color, request.Size, request.Quantity, request.Price);
        _unitOfWork.Product.Update(product);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return product.Variants.Single(x => x.Id == request.Id);
    }
}
