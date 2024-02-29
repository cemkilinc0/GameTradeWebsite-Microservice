using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Shared;
using RabbitMQ.Client.Events;
using Basket.Repositories;

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

            this.channel = connection.CreateModel();
            this.channel.QueueDeclare(queue: "basketUpdateQueue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            _logger.LogInformation("Created queue basketUpdateQueue");

            try
            {
                var consumer = new AsyncEventingBasicConsumer(this.channel);
                consumer.Received += async (model, ea) =>
                {
                    _logger.LogInformation("Recieved event from basketUpdateQueue");
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var updateEvent = JsonSerializer.Deserialize<ItemUpdatedIntegrationEvent>(message);
                    await ConsumeItemUpdate(updateEvent);
                };

                
                this.channel.BasicConsume(queue: "basketUpdateQueue",
                                        autoAck: true,
                                        consumer: consumer);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured before Recieved Event trigger: {ex}",ex);
            }        
        }
        public async Task ConsumeItemUpdate(ItemUpdatedIntegrationEvent updateEvent)
        {
            _logger.LogInformation("Consuming event from basketUpdateQueue");
            //Find all baskets with the item id and update them

        }
    }
}