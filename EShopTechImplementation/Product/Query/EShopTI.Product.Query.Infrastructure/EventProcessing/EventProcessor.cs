using EShopTI.Product.Common.Events;
using System.Text.Json;
using EShopTI.Product.Query.Infrastructure.Converters;
using EShopTI.Product.Query.Infrastructure.EventHandler;

namespace EShopTI.Product.Query.Infrastructure.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IEventHandler _eventHandler;

    public EventProcessor(IEventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }

    public void ProcessEvent(string message)
    {
        var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
        var @event = JsonSerializer.Deserialize<BaseEvent>(message, options);
        var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

        if (handlerMethod == null)
        {
            throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler method!");
        }

        handlerMethod.Invoke(_eventHandler, new object[] { @event });
    }
}