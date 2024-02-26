namespace Shared;

public class ItemIpdatedIntegrationEvent
{
    public int ItemId{ get; set; }
    public string ItemName { get; set; }
    public int Price { get; set; }
    public int QuantityAvailable { get; set; }
}
