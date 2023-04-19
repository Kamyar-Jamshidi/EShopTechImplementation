using EShopTI.Product.Command.Core;
using EShopTI.Product.Command.Core.Data.Repositories;
using EShopTI.Product.Command.Core.MessageBus;
using EShopTI.Product.Command.Infrastructure.Data;
using EShopTI.Product.Common.Events;
using MediatR;

namespace EShopTI.Product.Command.Infrastructure.EventSourcing;

public class EventSourcing<T> : IEventSourcing<T> where T : BaseEventSourcingAggregateRoot, new()
{
    private readonly IEventSourcingCommandRepository<EventModel<T>> _eventSourcingCommandRepository;
    private readonly IMessageBusClient _messageBusClient;
    private readonly IMediator _mediator;

    public EventSourcing(IEventSourcingCommandRepository<EventModel<T>> eventSourcingCommandRepository, IMessageBusClient messageBusClient, IMediator mediator)
    {
        _eventSourcingCommandRepository = eventSourcingCommandRepository;
        _messageBusClient = messageBusClient;
        _mediator = mediator;
    }

    public async Task<T> GetByIdAsync(string aggregateId, CancellationToken cancellationToken)
    {
        var aggregate = new T();
        var events = await GetEventsAsync(aggregateId, cancellationToken);

        if (events == null || !events.Any()) return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }

    public async Task RepublishEventsAsync(CancellationToken cancellationToken)
    {
        var aggregateIds = await GetAggregateIdsAsync(cancellationToken);

        if (aggregateIds == null || !aggregateIds.Any()) return;

        foreach (var aggregateId in aggregateIds)
        {
            var aggregate = await GetByIdAsync(aggregateId, cancellationToken);

            if (aggregate == null) continue;

            var events = await GetEventsAsync(aggregateId, cancellationToken);

            foreach (var @event in events)
            {
                _messageBusClient.Publish(@event);
            }
        }
    }

    public async Task SaveAsync(T aggregate, CancellationToken cancellationToken)
    {
        await SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version, cancellationToken);
        aggregate.MarkChangesAsCommitted();
    }

    private async Task<List<string>> GetAggregateIdsAsync(CancellationToken cancellationToken)
    {
        var events = await _eventSourcingCommandRepository.GetAllAsync(cancellationToken);

        if (events == null || !events.Any())
            throw new ArgumentNullException(nameof(_eventSourcingCommandRepository), "Could not retrieve events from the event store!");

        return events.Select(x => x.AggregateIdentifier).Distinct().ToList();
    }

    private async Task<List<BaseEvent>> GetEventsAsync(string aggregateId, CancellationToken cancellationToken)
    {
        var events = await _eventSourcingCommandRepository.GetByAggregateIdAsync(aggregateId, cancellationToken);

        if (events == null || !events.Any())
            throw new Exception("Incorrect post ID provided!");

        return events.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    private async Task SaveEventsAsync(string aggregateId, IEnumerable<BaseEvent> events, int expectedVersion, CancellationToken cancellationToken)
    {
        var prevEvents = await _eventSourcingCommandRepository.GetByAggregateIdAsync(aggregateId, cancellationToken);

        if (expectedVersion != -1 && prevEvents.Last().Version != expectedVersion)
            throw new Exception("ExpectedVersion is not valid!");

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventModel = new EventModel<T>
            {
                AggregateIdentifier = aggregateId,
                Version = version,
                EventType = @event.GetType().Name,
                EventData = @event
            };

            await _eventSourcingCommandRepository.Insert(eventModel, cancellationToken);

            _messageBusClient.Publish(@event);
        }
    }
}