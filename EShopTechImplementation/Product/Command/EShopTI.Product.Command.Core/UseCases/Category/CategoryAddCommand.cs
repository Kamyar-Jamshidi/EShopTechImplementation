using MediatR;
using CategoryDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Category.Category;

namespace EShopTI.Product.Command.Core.UseCases.Category;

public class CategoryAddCommand : IRequest<CategoryDomain>
{
    public CategoryAddCommand(string title)
    {
        Title = title;
    }

    public string Title { get; private set; }
}
