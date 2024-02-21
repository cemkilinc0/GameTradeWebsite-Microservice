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
        
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            var basket = await _basketRepository.GetBasket(userId);
            if(basket == null)
            {
                basket = new CustomerBasket(userId);
                await _basketRepository.UpdateBasket(basket);
            } 
            return Ok(basket);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateBasket([FromBody] CustomerBasket basket)
        {
            var updatedBasket = await _basketRepository.UpdateBasket(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            return Ok(await _basketRepository.DeleteBasket(userId));
        }
    }
}

