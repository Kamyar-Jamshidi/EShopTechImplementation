using EShopTI.Product.Query.Core.UseCases.Category;
using EShopTI.Product.Query.Core.UseCases.Product;
using MediatR;
using CategoryDomain = EShopTI.Product.Query.Core.Domain.Aggregates.Category.Category;
using ProductDomain = EShopTI.Product.Query.Core.Domain.Aggregates.Product.Product;

namespace EShopTI.Product.Query.Api.GraphQL;

public class Query
{
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductDomain> GetProducts([Service] IMediator mediator)
    {
        var request = new GetProductsQuery();
        return mediator.Send(request).Result;
    }

    [UseFiltering]
    [UseSorting]
    public IQueryable<CategoryDomain> GetCategories([Service] IMediator mediator)
    {
        var request = new GetCategoriesQuery();
        return mediator.Send(request).Result;
    }
}