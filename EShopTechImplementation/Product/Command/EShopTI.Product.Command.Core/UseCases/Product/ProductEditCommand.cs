using EShopTI.Product.Common.Domain.ValueObjects;
using MediatR;
using ProductDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Product.Product;

namespace EShopTI.Product.Command.Core.UseCases.Product;

public class ProductEditCommand : IRequest<ProductDomain>
{
    public ProductEditCommand(string id, string name, string categoryid, int quantity, Money price)
    {
        Id = id;
        Name = name;
        Categoryid = categoryid;
        Quantity = quantity;
        Price = price;
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Categoryid { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; set; }
}
