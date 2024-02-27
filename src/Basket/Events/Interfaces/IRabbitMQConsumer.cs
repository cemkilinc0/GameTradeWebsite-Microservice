using Shared;

public interface IRabbitMQConsumer
{
        void ConsumeItemUpdate(ItemUpdatedIntegrationEvent event);
}
