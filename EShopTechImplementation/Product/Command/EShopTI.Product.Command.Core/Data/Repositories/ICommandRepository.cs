using EShopTI.Product.Common;

namespace EShopTI.Product.Command.Core.Data.Repositories;

public interface ICommandRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task Insert(T entity, CancellationToken cancellationToken);
    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task Update(T model, CancellationToken cancellationToken);
}
