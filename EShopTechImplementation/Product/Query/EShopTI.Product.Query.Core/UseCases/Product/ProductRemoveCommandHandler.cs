using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product;

public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductRemoveCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(request.Id, cancellationToken);
        product.Delete();
        _unitOfWork.Product.Update(product);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
