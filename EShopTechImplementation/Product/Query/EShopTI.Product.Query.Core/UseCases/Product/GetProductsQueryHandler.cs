using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IQueryable<Domain.Aggregates.Product.Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IQueryable<Domain.Aggregates.Product.Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Product.ToList();
    }
}