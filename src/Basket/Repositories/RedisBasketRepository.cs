using StackExchange.Redis;
using Newtonsoft.Json;
using Basket.Repositories.Interfaces;
using Basket.Entities;

namespace Basket.Repositories
{
    public class RedisBasketRepository : IBasketRepository
    {
        private IDatabase _database;

        public RedisBasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket> GetBasket(string customerId)
        {
            var data = await _database.StringGetAsync(customerId);
            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.UserId, JsonConvert.SerializeObject(basket));
            if(!created) return null;
            return await GetBasket(basket.UserId);
        }

        public async Task<bool> DeleteBasket(string customerId)
        {
            return await _database.KeyDeleteAsync(customerId);
        }
    }
}

