using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Shared;

namespace Basket.Events
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly IModel channel;
        private readonly ILogger<RabbitMQConsumer> _logger;
        public RabbitMQConsumer(IConnection connection, ILogger<RabbitMQConsumer> logger)
        {
            _logger = logger;
            _logger.LogInformation("Creating basketUpdateQueue");

            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "basketUpdateQueue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                _logger.LogInformation("Recieved event from basketUpdateQueue");
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var updateEvent = JsonSerializer.Deserialize<ItemUpdatedIntegrationEvent>(message);
                ConsumeItemUpdate(updateEvent);
            };

        }
        public void ConsumeItemUpdate(ItemUpdatedIntegrationEvent updateEvent)
        {
            _logger.LogInformation("Consuming event from basketUpdateQueue");
        }
    }
}