using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product;

public class ProductEditCommandHandler : IRequestHandler<ProductEditCommand, Domain.Aggregates.Product.Product>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductEditCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Aggregates.Product.Product> Handle(ProductEditCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(request.Id, cancellationToken);
        product.Edit(request.Name, request.CategoryId, request.Quantity, request.Price);
        _unitOfWork.Product.Update(product);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return product;
    }
}
