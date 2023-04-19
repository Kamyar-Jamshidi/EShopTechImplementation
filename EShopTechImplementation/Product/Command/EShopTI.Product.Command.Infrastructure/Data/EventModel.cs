using EShopTI.Product.Common.Events;
using EShopTI.Product.Command.Core;

namespace EShopTI.Product.Command.Infrastructure.Data;

public class EventModel<T> where T : BaseEventSourcingAggregateRoot
{
    public EventModel()
    {
        Id = Guid.NewGuid().ToString();
        AggregateType = typeof(T).Name;
        CreateAt = DateTime.Now;
    }

    public string Id { get; set; }
    public DateTime CreateAt { get; set; }
    public string AggregateIdentifier { get; set; }
    public string AggregateType { get; set; }
    public int Version { get; set; }
    public string EventType { get; set; }
    public BaseEvent EventData { get; set; }
}