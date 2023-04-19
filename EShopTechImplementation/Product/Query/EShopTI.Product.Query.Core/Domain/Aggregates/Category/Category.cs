using EShopTI.Product.Common.Domain.Aggregates.Category;

namespace EShopTI.Product.Query.Core.Domain.Aggregates.Category;

public class Category : BaseCategory
{
	public Category()
	{
		Products = new HashSet<Product.Product>();
	}

	public Category(string id, string title) : base(id, title) 
	{
	}

    public ICollection<Product.Product> Products { get; private set; }
}
