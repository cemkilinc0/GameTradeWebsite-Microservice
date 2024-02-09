using Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IGameItemRepository _gameItemRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IGameItemRepository gameItemRepository, ILogger<CatalogController> logger)
        {
            _gameItemRepository = gameItemRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all game items");
            var items = await _gameItemRepository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Getting game item with id: {id}");
            var item = await _gameItemRepository.GetById(id);
            if (item == null)
            {
                _logger.LogWarning($"Game item with id: {id} not found.");
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameItem gameItem)
        {
            _logger.LogInformation($"Game item with name: {gameItem.ItemName} created");
            var newItem = await _gameItemRepository.Create(gameItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.ItemId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GameItem gameItem)
        {
            if (id != gameItem.ItemId)
            {
                _logger.LogWarning($"Update request with mismatched Id: {id}");
                return BadRequest();
            }

            _logger.LogInformation($"Updating game item with Id: {id}");
            var existingItem = await _gameItemRepository.GetById(id);
            if (existingItem == null)
            {
                _logger.LogWarning($"Game item with Id: {id} not found for update.");
                return NotFound();
            }

            await _gameItemRepository.Update(gameItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Attempting to delete game item with Id: {id}");
            var item = await _gameItemRepository.GetById(id);
            if (item == null)
            {
                _logger.LogWarning($"Game item with Id: {id} not found for deletion.");
                return NotFound();
            }

            await _gameItemRepository.Delete(id);
            return NoContent();
        }
    }
}