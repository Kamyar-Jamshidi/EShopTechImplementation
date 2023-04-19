namespace EShopTI.Product.Command.Core.Data.Repositories;

public interface IEventSourcingCommandRepository<T> where T : class
{
    Task<IEnumerable<T>> GetByAggregateIdAsync(string aggregateId, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task Insert(T entity, CancellationToken cancellationToken);
}