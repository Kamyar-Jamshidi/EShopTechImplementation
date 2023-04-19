using EShopTI.Product.Common.Events;

namespace EShopTI.Product.Command.Core.MessageBus;

public interface IMessageBusClient
{
    void Dispose();
    void Publish(BaseEvent @event);
}