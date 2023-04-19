using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Category;

public class CategoryRemoveCommand : IRequest<Domain.Aggregates.Category.Category>
{
    public CategoryRemoveCommand(string id)
    {
        Id = id;
    }

    public string Id { get; private set; }
}
