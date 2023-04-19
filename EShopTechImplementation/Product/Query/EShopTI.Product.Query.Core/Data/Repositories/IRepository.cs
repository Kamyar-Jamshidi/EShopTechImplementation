using EShopTI.Product.Common.Domain;
using System.Linq.Expressions;

namespace EShopTI.Product.Query.Core.Data.Repositories;

public interface IRepository<T> where T : BaseAggregateRoot
{
    Task<bool> Any();
    Task<bool> Any(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    Task<T> First();
    Task<T> First(Expression<Func<T, bool>> predicate);
    Task<T> FirstOrDefault();
    Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);
    void Insert(T entity);
    Task<T> Last();
    Task<T> Last(Expression<Func<T, bool>> predicate);
    Task<T> LastOrDefault();
    Task<T> LastOrDefault(Expression<Func<T, bool>> predicate);
    Task<T> Single(Expression<Func<T, bool>> predicate);
    Task<T> SingleOrDefault(Expression<Func<T, bool>> predicate);
    IQueryable<T> ToList();
    void Update(T entity);
}
