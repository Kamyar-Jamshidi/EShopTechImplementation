using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.ValueObjects;

namespace EShopTI.Product.Common.Events.Product;

public class ProductVariantCreateEvent : BaseEvent
{
	public ProductVariantCreateEvent() : base(nameof(ProductVariantCreateEvent))
	{
	}

    public Colors Color { get; set; }
    public Sizes Size { get; set; }
    public string ProductId { get; set; }
	public int Quantity { get; set; }
    public Money Price { get; set; }
}
