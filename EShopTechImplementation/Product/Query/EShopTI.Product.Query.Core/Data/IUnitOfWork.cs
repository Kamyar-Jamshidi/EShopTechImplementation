using EShopTI.Product.Query.Core.Data.Repositories;
using EShopTI.Product.Query.Core.Domain.Aggregates.Category;

namespace EShopTI.Product.Query.Core.Data;

public interface IUnitOfWork
{
    IRepository<Category> Category { get; }
    IRepository<Domain.Aggregates.Product.Product> Product { get; }

    Task<int> CompleteAsync(CancellationToken cancellationToken);
}
