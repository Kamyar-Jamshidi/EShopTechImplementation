using EShopTI.Product.Common.Domain.Aggregates.Category;

namespace EShopTI.Product.Command.Core.Domain.Aggregates.Category;

public class Category : BaseCategory
{
    public Category() : base()
    {
    }

    public Category(string id, string title) : base(id, title)
    {
    }
}
