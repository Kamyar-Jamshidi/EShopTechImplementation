using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.ValueObjects;

namespace EShopTI.Product.Common.Events.Product;

public class ProductVariantEditEvent : BaseEvent
{
    public ProductVariantEditEvent() : base(nameof(ProductVariantEditEvent))
    {
    }

    public Colors Color { get; set; }
    public Sizes Size { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; }
    public string ProductId { get; set; }
}
