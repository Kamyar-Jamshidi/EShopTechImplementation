using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Category;

public class CategoryEditCommand : IRequest<Domain.Aggregates.Category.Category>
{
    public CategoryEditCommand(string id, string title)
    {
        Id = id;
        Title = title;
    }

    public string Id { get; private set; }
    public string Title { get; private set; }
}
