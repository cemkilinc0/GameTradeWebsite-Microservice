using StackExchange.Redis;
using Newtonsoft.Json;
using Basket.Repositories.Interfaces;
using Basket.Entities;

namespace Basket.Repositories
{
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly ILogger<RedisBasketRepository> _logger;

        public RedisBasketRepository(IConnectionMultiplexer redis, ILogger<RedisBasketRepository> logger)
        {
            _database = redis.GetDatabase();
            _logger = logger;
        }

        public async Task<CustomerBasket> GetBasket(string customerId)
        {
            _logger.LogInformation("Retrieving basket for user {userId}", customerId);
            var data = await _database.StringGetAsync(customerId);
            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
            _logger.LogInformation("Updating basket for user {userId}", basket.UserId);
            var created = await _database.StringSetAsync(basket.UserId, JsonConvert.SerializeObject(basket));
            if(!created) return null;
            return await GetBasket(basket.UserId);
        }

        public async Task<bool> DeleteBasket(string customerId)
        {
            _logger.LogInformation("Deleting basket of user {userId}", customerId);
            return await _database.KeyDeleteAsync(customerId);
        }
    }
}

