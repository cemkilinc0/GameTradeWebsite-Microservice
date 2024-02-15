using System.ComponentModel.DataAnnotations;

namespace Basket.Entities
{
    public class BasketItem
    {
        [Key]
        public Guid BasketItemId { get; set; }
        public Guid BasketId { get; set; }
        public Guid GameItemId { get; set; }  // Reference to an Item in the Catalog service
        public string GameItemName { get; set; }
        public int Quantity { get; set; }
        public string ImageURL{ get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

