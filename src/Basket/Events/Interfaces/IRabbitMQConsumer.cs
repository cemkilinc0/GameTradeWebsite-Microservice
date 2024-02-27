using Shared;

namespace Basket.Events
{
        public interface IRabbitMQConsumer
        {
                void ConsumeItemUpdate(ItemUpdatedIntegrationEvent updateEvent);
        }
}

