using EShopTI.Product.Common.Domain;
using EShopTI.Product.Common.Events;
using System.Text.Json.Serialization;

namespace EShopTI.Product.Command.Core;

public abstract class BaseEventSourcingAggregateRoot : BaseAggregateRoot
{
    [JsonIgnore]
    public int Version { get; set; } = -1;

    [JsonIgnore]
    private readonly List<BaseEvent> _changes = new();

    public IEnumerable<BaseEvent> GetUncommittedChanges()
    {
        return _changes;
    }

    public void MarkChangesAsCommitted()
    {
        _changes.Clear();
    }

    private void ApplyChange(BaseEvent @event, bool isNew)
    {
        var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

        if (method == null)
        {
            throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}!");
        }

        method.Invoke(this, new object[] { @event });

        if (isNew)
        {
            _changes.Add(@event);
        }
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChange(@event, false);
        }
    }
}
