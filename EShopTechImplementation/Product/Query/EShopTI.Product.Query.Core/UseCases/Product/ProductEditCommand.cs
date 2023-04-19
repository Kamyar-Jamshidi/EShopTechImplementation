using EShopTI.Product.Common.Domain.ValueObjects;
using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product;

public class ProductEditCommand : IRequest<Domain.Aggregates.Product.Product>
{
    public ProductEditCommand(string id, string name, string categoryId, int quantity, Money price)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
        Quantity = quantity;
        Price = price;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public string CategoryId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
}
