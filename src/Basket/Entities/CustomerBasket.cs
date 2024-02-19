using Basket.Entities;

namespace Basket.Entities
{
    public class CustomerBasket
    {
        public string UserId { get; set; }
        public List<BasketItem> GameItems { get; set; } = new List<BasketItem>();

        public CustomerBasket(string customerId)
        {
            UserId = customerId;
            GameItems = new List<BasketItem>();
        }
    }
}