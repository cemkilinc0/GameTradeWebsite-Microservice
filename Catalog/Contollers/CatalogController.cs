
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

        public CatalogController(IGameItemRepository gameItemRepository)
        {
            _gameItemRepository = gameItemRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _gameItemRepository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _gameItemRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(GameItem gameItem)
        {
            var newItem = _gameItemRepository.Create(gameItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.ItemId }, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GameItem gameItem)
        {
            if (id != gameItem.ItemId)
            {
                return BadRequest();
            }

            var existingItem = _gameItemRepository.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _gameItemRepository.Update(gameItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _gameItemRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            _gameItemRepository.Delete(id);
            return NoContent();
        }
    }
}