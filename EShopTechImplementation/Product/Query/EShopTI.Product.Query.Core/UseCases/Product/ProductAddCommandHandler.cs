using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product;

public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, Domain.Aggregates.Product.Product>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductAddCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Aggregates.Product.Product> Handle(ProductAddCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Aggregates.Product.Product(request.Id, request.Name, request.CategoryId, request.Quantity, request.Price);
        _unitOfWork.Product.Insert(product);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return product;
    }
}
