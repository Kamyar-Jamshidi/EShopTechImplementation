using MediatR;

namespace EShopTI.Product.Query.Core.UseCases.Category;

public class GetCategoriesQuery : IRequest<IQueryable<Domain.Aggregates.Category.Category>>
{
}