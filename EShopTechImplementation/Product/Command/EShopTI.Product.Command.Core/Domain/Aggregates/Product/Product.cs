using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.ValueObjects;
using EShopTI.Product.Common.Events.Product;

namespace EShopTI.Product.Command.Core.Domain.Aggregates.Product;

public class Product : BaseEventSourcingAggregateRoot
{
    public string Name { get; private set; }
    public string CategoryId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

    private readonly Dictionary<string, ProductVariant> _variants = new();
    public IReadOnlyList<ProductVariant> Variants => _variants.Values.ToList().AsReadOnly();

    public Product()
    {

    }

    public Product(string id, string name, string categoryId, int quantity, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException($"The value of {nameof(name)} cannot be null or empty. Please provide a valid {nameof(name)}!");
        }

        RaiseEvent(new ProductCreateEvent
        {
            CategoryId = categoryId,
            Name = name,
            Id = id,
            Quantity = quantity,
            Price = price
        });
    }

    #region Product

    public void Apply(ProductCreateEvent @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        CategoryId = @event.CategoryId;
        Quantity = @event.Quantity;
        Price = @event.Price;
    }

    public void Edit(string name, string categoryId, int quantity, Money price)
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException($"This product is deletet and you cannot update it!");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException($"The value of {nameof(name)} cannot be null or empty. Please provide a valid {nameof(name)}!");
        }

        if (this.Quantity < _variants.Where(x => !x.Value.IsDeleted).Select(x => x.Value.Quantity).Sum())
        {
            throw new InvalidOperationException("The quantity of product cannot be less then sum of it's product variants quantity!");
        }

        RaiseEvent(new ProductEditEvent
        {
            Id = Id,
            Name = name,
            CategoryId = categoryId,
            Quantity = quantity,
            Price = price
        });
    }

    public void Apply(ProductEditEvent @event)
    {
        Id = @event.Id;
        Name = @event.Name;
        CategoryId = @event.CategoryId;
        Quantity = @event.Quantity;
        Price = @event.Price;
    }

    public override void Delete()
    {
        RaiseEvent(new ProductRemoveEvent { Id = Id });
    }

    public void Apply(ProductRemoveEvent @event)
    {
        Id = @event.Id;
        IsDeleted = true;
    }

    #endregion

    #region Product Variant

    public void AddVariant(string id, Colors color, Sizes size, int quantity, Money price)
    {
        if (this.Quantity < (_variants.Where(x => !x.Value.IsDeleted).Select(x => x.Value.Quantity).Sum() + quantity))
        {
            throw new InvalidOperationException("The quantity of product cannot be less then sum of it's product variants quantity!");
        }

        RaiseEvent(new ProductVariantCreateEvent
        {
            Id = id,
            Color = color,
            Size = size,
            Quantity = quantity,
            Price = price,
            ProductId = this.Id
        });
    }

    public void Apply(ProductVariantCreateEvent @event)
    {
        _variants.Add(@event.Id, new ProductVariant(@event.Id, @event.Color, @event.Size, Id, @event.Quantity, @event.Price));
    }

    public void EditVariant(string id, Colors color, Sizes size, int quantity, Money price)
    {
        if (this.Quantity < (_variants.Where(x => !x.Value.IsDeleted && x.Key != id).Select(x => x.Value.Quantity).Sum() + quantity))
        {
            throw new InvalidOperationException("The quantity of product cannot be less then sum of it's product variants quantity!");
        }

        RaiseEvent(new ProductVariantEditEvent
        {
            Id = id,
            Color = color,
            Size = size,
            Quantity = quantity,
            Price = price,
            ProductId = this.Id
        });
    }

    public void Apply(ProductVariantEditEvent @event)
    {
        _variants[@event.Id] = new ProductVariant(@event.Id, @event.Color, @event.Size, this.Id, @event.Quantity, @event.Price);
    }

    public void DeleteVariant(string VariantId)
    {
        RaiseEvent(new ProductVariantRemoveEvent
        {
            ProductId = this.Id
        });
    }

    public void Apply(ProductVariantRemoveEvent @event)
    {
        var variant = _variants[@event.Id];
        variant.Delete();
        _variants[@event.Id] = variant;

        this.Quantity -= variant.Quantity;
    }

    #endregion
}