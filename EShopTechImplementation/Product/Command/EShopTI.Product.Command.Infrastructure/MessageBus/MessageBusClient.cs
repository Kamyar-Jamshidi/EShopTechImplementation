using EShopTI.Product.Command.Core.MessageBus;
using EShopTI.Product.Common.Config;
using EShopTI.Product.Common.Events;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EShopTI.Product.Common.MessageBus;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabitMQConfig _config;

    public MessageBusClient(IOptions<RabitMQConfig> config)
    {
        _config = config.Value;
        var factory = new ConnectionFactory()
        {
            HostName = _config.RabbitMQHost,
            Port = int.Parse(_config.RabbitMQPort)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: _config.Exchange, type: ExchangeType.Fanout);
    }

    public void Publish(BaseEvent @event)
    {
        var message = JsonSerializer.Serialize(@event, @event.GetType());

        if (_connection.IsOpen)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _config.Exchange,
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        }
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}