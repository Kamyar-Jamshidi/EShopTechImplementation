using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Category;

public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, Domain.Aggregates.Category.Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryAddCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Aggregates.Category.Category> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
    {
        var category = new Domain.Aggregates.Category.Category(request.Id, request.Title);
        _unitOfWork.Category.Insert(category);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return category;
    }
}
