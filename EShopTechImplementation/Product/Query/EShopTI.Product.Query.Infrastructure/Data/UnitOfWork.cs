using EShopTI.Product.Query.Core.Data;
using EShopTI.Product.Query.Core.Data.Repositories;
using EShopTI.Product.Query.Core.Domain.Aggregates.Category;

namespace EShopTI.Product.Query.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductQueryDbContext _context;
    private readonly IRepository<Category> _category;
    private readonly IRepository<Core.Domain.Aggregates.Product.Product> _product;

    public UnitOfWork(ProductQueryDbContext context,
        IRepository<Category> category,
        IRepository<Core.Domain.Aggregates.Product.Product> product)
    {
        _context = context;
        _category = category;
        _product = product;
    }

    public IRepository<Category> Category => _category;
    public IRepository<Core.Domain.Aggregates.Product.Product> Product => _product;

    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
