using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Catalog.Entities;
using Shared;

namespace Catalog.Events
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IModel channel;
        private readonly ILogger<RabbitMQPublisher> _logger;
        public RabbitMQPublisher(IConnection connection,ILogger<RabbitMQPublisher> logger)
        {
            _logger = logger;
            _logger.LogInformation("Creating basketUpdateQueueueueueue");
            
            this.channel = connection.CreateModel();
            channel.QueueDeclare(queue: "basketUpdateQueue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
        }

        public void PublishItemUpdate(GameItem gameItem)
        {
            _logger.LogInformation("Creating ItemUpdatedIntegrationEvent");
            //Create the integration event
            var integrationEvent = new ItemUpdatedIntegrationEvent{ItemId = gameItem.ItemId,
                                                                   ItemName = gameItem.ItemName,
                                                                   Price = gameItem.Price,
                                                                   QuantityAvailable = gameItem.QuantityAvailable};


            var message = JsonSerializer.Serialize(integrationEvent);
            var body = Encoding.UTF8.GetBytes(message);

            _logger.LogInformation("Publishing ItemUpdatedIntegrationEvent");
            channel.BasicPublish(exchange: "",
                                routingKey: "basketUpdateQueue",
                                basicProperties: null,
                                body: body);
        }
    }
}