using EShopTI.Product.Common.Domain;
using EShopTI.Product.Query.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EShopTI.Product.Query.Infrastructure.Data.Repositories;

public class PostgreSqlRepository<T> : IRepository<T> where T : BaseAggregateRoot
{
    private readonly ProductQueryDbContext _context;

    public PostgreSqlRepository(ProductQueryDbContext context)
    {
        _context = context;
    }

    #region Public

    public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().CountAsync(predicate, cancellationToken);
    }

    public IQueryable<T> ToList()
    {
        return _context.Set<T>().AsQueryable();
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }

    public async Task<T> Single(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleAsync(predicate);
    }

    public async Task<T> SingleOrDefault(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public async Task<T> First()
    {
        return await _context.Set<T>().FirstAsync();
    }

    public async Task<T> First(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstAsync(predicate);
    }

    public async Task<T> FirstOrDefault()
    {
        return await _context.Set<T>().FirstOrDefaultAsync();
    }

    public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> Last()
    {
        return await _context.Set<T>().LastAsync();
    }

    public async Task<T> Last(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().LastAsync(predicate);
    }

    public async Task<T> LastOrDefault()
    {
        return await _context.Set<T>().LastOrDefaultAsync();
    }

    public async Task<T> LastOrDefault(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().LastOrDefaultAsync(predicate);
    }

    public async Task<bool> Any()
    {
        return await _context.Set<T>().AnyAsync();
    }

    public async Task<bool> Any(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }

    public void Insert(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    #endregion
}