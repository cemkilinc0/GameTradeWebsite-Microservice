using Basket.Entities;
using System.ComponentModel.DataAnnotations;

namespace Basket.Entities
{
    public class CustomerBasket
    {
        [Key]
        public Guid BasketId { get; set; }
        public string UserId { get; set; }
        public List<BasketItem> GameItems { get; set; } = new List<BasketItem>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}