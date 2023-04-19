using EShopTI.Product.Common.Domain.ValueObjects;

namespace EShopTI.Product.Common.Domain.Aggregates.Product;

public abstract class BaseProductVariant : BaseEntity
{
    public Colors Color { get; private set; }
    public Sizes Size { get; private set; }
    public string ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

    public BaseProductVariant()
    {
    }

    public BaseProductVariant(string id, Colors color, Sizes size, string productId, int quantity, Money price)
    {
        Id = id;
        Color = color;
        Size = size;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}
