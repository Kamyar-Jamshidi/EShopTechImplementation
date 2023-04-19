using EShopTI.Product.Common.Domain.ValueObjects;

namespace EShopTI.Product.Common.Events.Product;

public class ProductCreateEvent : BaseEvent
{
	public ProductCreateEvent() : base(nameof(ProductCreateEvent))
	{
	}

	public string Name { get; set; }
	public string CategoryId { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; }
}
