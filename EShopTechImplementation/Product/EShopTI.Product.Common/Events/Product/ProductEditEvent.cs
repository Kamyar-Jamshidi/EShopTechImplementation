using EShopTI.Product.Common.Domain.ValueObjects;

namespace EShopTI.Product.Common.Events.Product;

public class ProductEditEvent : BaseEvent
{
    public ProductEditEvent() : base(nameof(ProductEditEvent))
    {
    }

    public string CategoryId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; }
}
