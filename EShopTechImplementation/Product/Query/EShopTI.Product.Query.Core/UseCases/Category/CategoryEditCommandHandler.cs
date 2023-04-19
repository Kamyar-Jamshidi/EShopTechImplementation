using EShopTI.Product.Query.Core.Data;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Category;

public class CategoryEditCommandHandler : IRequestHandler<CategoryEditCommand, Domain.Aggregates.Category.Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryEditCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Aggregates.Category.Category> Handle(CategoryEditCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Category.GetByIdAsync(request.Id, cancellationToken);

        if (category == null)
        {
            throw new Exception($"Cannot find category with id:{request.Id}!");
        }

        category.Edit(request.Title);

        _unitOfWork.Category.Update(category);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return category;
    }
}
