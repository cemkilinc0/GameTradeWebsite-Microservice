
using Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories.Interfaces;

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
        public IActionResult GetAll()
        {
            _logger.LogInformation("Getting all game items");
            var items = _gameItemRepository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation($"Getting game item with id: {id}");
            var item = _gameItemRepository.GetById(id);
            if (item == null)
            {
                _logger.LogWarning($"Game item with id: {id} not found.");
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(GameItem gameItem)
        {
            _logger.LogInformation($"Game item with name: {gameItem.ItemName} created");
            var newItem = _gameItemRepository.Create(gameItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.ItemId }, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GameItem gameItem)
        {
            if (id != gameItem.ItemId)
            {
                _logger.LogWarning($"Update request with mismatched Id: {id}");
                return BadRequest();
            }

            _logger.LogInformation($"Updating game item with Id: {id}");
            var existingItem = _gameItemRepository.GetById(id);
            if (existingItem == null)
            {
                _logger.LogWarning($"Game item with Id: {id} not found for update.");
                return NotFound();
            }

            _gameItemRepository.Update(gameItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"Attempting to delete game item with Id: {id}");
            var item = _gameItemRepository.GetById(id);
            if (item == null)
            {
                _logger.LogWarning($"Game item with Id: {id} not found for deletion.");
                return NotFound();
            }

            _gameItemRepository.Delete(id);
            return NoContent();
        }
    }
}