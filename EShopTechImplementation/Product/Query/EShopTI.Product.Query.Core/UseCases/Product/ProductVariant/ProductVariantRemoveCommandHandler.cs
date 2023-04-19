using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product.ProductVariant;

public class ProductVariantRemoveCommandHandler : IRequestHandler<ProductVariantRemoveCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductVariantRemoveCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProductVariantRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(request.ProductId, cancellationToken);
        product.DeleteVariant(request.Id);
        _unitOfWork.Product.Update(product);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
