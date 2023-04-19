using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using EShopTI.Product.Common.Config;
using System.Text;
using EShopTI.Product.Query.Infrastructure.EventProcessing;

namespace EShopTI.Product.Query.Infrastructure;

public class MessageBusSubscriber : BackgroundService
{
    private readonly RabitMQConfig _config;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusSubscriber(IOptions<RabitMQConfig> config, IEventProcessor eventProcessor)
    {
        _config = config.Value;
        _eventProcessor = eventProcessor;

        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory() { HostName = _config.RabbitMQHost, Port = int.Parse(_config.RabbitMQPort) };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: _config.Exchange, type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _queueName,
            exchange: _config.Exchange,
            routingKey: "");
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (ModuleHandle, ea) =>
        {
            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            _eventProcessor.ProcessEvent(notificationMessage);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }

        base.Dispose();
    }
}