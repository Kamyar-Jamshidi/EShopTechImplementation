using MediatR;
using CategoryDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Category.Category;

namespace EShopTI.Product.Command.Core.UseCases.Category;

public class CategoryRemoveCommand : IRequest
{
    public CategoryRemoveCommand(string id)
    {
        Id = id;
    }

    public string Id { get; private set; }
}
