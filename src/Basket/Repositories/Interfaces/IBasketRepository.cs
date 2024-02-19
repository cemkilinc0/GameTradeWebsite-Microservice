using Basket.Entities

namespace Basket.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasket(string customerId);
        Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
        Task<bool> DeleteBasket(string customerId);
    }
}