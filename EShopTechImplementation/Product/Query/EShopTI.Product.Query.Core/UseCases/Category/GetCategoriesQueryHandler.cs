using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Category;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IQueryable<Domain.Aggregates.Category.Category>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IQueryable<Domain.Aggregates.Category.Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Category.ToList();
    }
}
