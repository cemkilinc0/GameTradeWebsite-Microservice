
namespace Shared
{
    public class ItemUpdatedIntegrationEvent
    {
        public int ItemId{ get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
    }
}

