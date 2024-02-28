using Shared;

namespace Basket.Events
{
        public interface IRabbitMQConsumer
        {
                Task ConsumeItemUpdate(ItemUpdatedIntegrationEvent updateEvent);
        }
}

