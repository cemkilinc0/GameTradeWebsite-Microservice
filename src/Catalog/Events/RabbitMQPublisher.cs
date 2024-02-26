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
        public RabbitMQPublisher(IConnection connection)
        {
            this.channel = connection.CreateModel();
            channel.QueueDeclare(queue: "basketUpdateQueue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
        }

        public void PublishItemUpdate(GameItem gameItem)
        {
            var message = JsonSerializer.Serialize(gameItem);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                routingKey: "basketUpdateQueue",
                                basicProperties: null,
                                body: body);
        }
    }
}