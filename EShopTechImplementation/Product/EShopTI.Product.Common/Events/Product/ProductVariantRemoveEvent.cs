namespace EShopTI.Product.Common.Events.Product;

public class ProductVariantRemoveEvent : BaseEvent
{
	public ProductVariantRemoveEvent() : base(nameof(ProductVariantRemoveEvent))
	{
	}

    public string ProductId { get; set; }
}
