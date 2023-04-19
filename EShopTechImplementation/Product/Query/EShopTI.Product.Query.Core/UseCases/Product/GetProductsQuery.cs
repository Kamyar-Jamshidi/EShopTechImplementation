using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Product;

public class GetProductsQuery : IRequest<IQueryable<Domain.Aggregates.Product.Product>>
{
}