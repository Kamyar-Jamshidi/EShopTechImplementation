using EShopTI.Product.Command.Core;

namespace EShopTI.Product.Command.Infrastructure.EventSourcing;

public interface IEventSourcing<T> where T : BaseEventSourcingAggregateRoot, new()
{
    Task<T> GetByIdAsync(string aggregateId, CancellationToken cancellationToken);
    Task RepublishEventsAsync(CancellationToken cancellationToken);
    Task SaveAsync(T aggregate, CancellationToken cancellationToken);
}