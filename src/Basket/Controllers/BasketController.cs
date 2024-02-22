using Basket.Entities;
using Basket.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<BasketController> _logger;
        
        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            _logger.LogInformation("GetBasket called for user {userId}", userId);
            var basket = await _basketRepository.GetBasket(userId);
            if(basket == null)
            {
                _logger.LogInformation("Basket not found for user {userId}, creating empty basket.", userId);
                basket = new CustomerBasket(userId);
                await _basketRepository.UpdateBasket(basket);
            } 
            return Ok(basket);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateBasket([FromBody] CustomerBasket basket)
        {
            _logger.LogInformation("UpdateBasket called for user {userId}", basket.UserId);
            var updatedBasket = await _basketRepository.UpdateBasket(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            _logger.LogInformation("DeleteBasket called for user {userId}", userId);
            return Ok(await _basketRepository.DeleteBasket(userId));
        }
    }
}

