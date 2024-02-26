using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Basket.Events
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly IModel channel;

        public RabbitMQConsumer(IConnection connection)
        {
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "basketUpdateQueue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var updateEvent = JsonSerializer.Deserialize<ItemUpdateIntegrationEvent>(message);
                ConsumeItemUpdate(updateEvent);
            };

        }
        public void ConsumeItemUpdate()
        {

        }
    }
}