using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Category;

public class CategoryAddCommand : IRequest<Domain.Aggregates.Category.Category>
{
    public CategoryAddCommand(string id, string title)
    {
        Title = title;
        Id = id;
    }

    public string Id { get; private set; }
    public string Title { get; private set; }
}
