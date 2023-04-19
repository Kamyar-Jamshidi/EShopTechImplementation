using MediatR;
using CategoryDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Category.Category;

namespace EShopTI.Product.Command.Core.UseCases.Category;

public class CategoryEditCommand : IRequest<CategoryDomain>
{
    public CategoryEditCommand(string id, string title)
    {
        Id = id;
        Title = title;
    }

    public string Id { get; private set; }
    public string Title { get; private set; }
}
