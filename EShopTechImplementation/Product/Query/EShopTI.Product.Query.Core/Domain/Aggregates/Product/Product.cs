using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Domain.ValueObjects;
using EShopTI.Product.Query.Core.Domain.Aggregates.Category;

namespace EShopTI.Product.Query.Core.Domain.Aggregates.Product;

public class Product : BaseAggregateRoot
{
    public string Name { get; private set; }
    public string CategoryId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

    private readonly List<ProductVariant> _variants = new();
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

    public Category.Category Category { get; private set; }

    public Product()
    {
    }

    public Product(string id, string name, string categoryId, int quantity, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException($"The value of {nameof(name)} cannot be null or empty. Please provide a valid {nameof(name)}!");
        }

        Id = id;
        Name = name;
        CategoryId = categoryId;
        Quantity = quantity;
        Price = price;
    }

    #region Product

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

        if (this.Quantity < _variants.Where(x => !x.IsDeleted).Select(x => x.Quantity).Sum())
        {
            throw new InvalidOperationException("The quantity of product cannot be less then sum of it's product variants quantity!");
        }

        Name = name;
        CategoryId = categoryId;
        Quantity = quantity;
        Price = price;
    }

    #endregion

    #region Product Variant

    public void AddVariant(string id, Colors color, Sizes size, int quantity, Money price)
    {
        if (this.Quantity < (_variants.Where(x => !x.IsDeleted).Select(x => x.Quantity).Sum() + quantity))
        {
            throw new InvalidOperationException("The quantity of product cannot be less then sum of it's product variants quantity!");
        }

        _variants.Add(new ProductVariant(id, color, size, this.Id, quantity, price));
    }

    public void EditVariant(string id, Colors color, Sizes size, int quantity, Money price)
    {
        if (this.Quantity < (_variants.Where(x => !x.IsDeleted && x.Id != id).Select(x => x.Quantity).Sum() + quantity))
        {
            throw new InvalidOperationException("The quantity of product cannot be less then sum of it's product variants quantity!");
        }

        _variants[_variants.FindIndex(x => x.Id == id)] = new ProductVariant(id, color, size, this.Id, quantity, price);
    }

    public void DeleteVariant(string VariantId)
    {
        var index = _variants.FindIndex(x => x.Id == VariantId);
        _variants[index].Delete();

        this.Quantity -= _variants[index].Quantity;
    }

    #endregion
}
