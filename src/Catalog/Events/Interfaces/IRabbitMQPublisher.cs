using Catalog.Entities;

public interface IRabbitMQPublisher
{
    void PublishItemUpdate(GameItem gameItem);
}