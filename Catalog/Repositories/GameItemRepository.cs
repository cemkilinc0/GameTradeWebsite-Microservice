using System;
using Catalog.Repositories.Interfaces;
using Catalog.Entities;
using Catalog.Data;

namespace Catalog.Repositories
{
    public class GameItemRepository : IGameItemRepository
    {
        private readonly CatalogDbContext _context;
        private readonly ILogger<GameItemRepository> _logger;

        public GameItemRepository(CatalogDbContext context, ILogger<GameItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public GameItem Create(GameItem gameItem)
        {
            _logger.LogInformation($"GameItem with id. {gameItem.ItemId} getting created");
            _context.GameItems.Add(gameItem);
            _context.SaveChanges();
            return gameItem;
        }

        public IEnumerable<GameItem> GetAll()
        {
            _logger.LogInformation("Getting all game items");
            return _context.GameItems.ToList();
        }

        public GameItem GetById(int id)
        {
            _logger.LogInformation($"Fetching game item with id: {id}");
            return _context.GameItems.FirstOrDefault(item => item.ItemId == id);
        }

        public GameItem Update(GameItem gameItem)
        {
            _logger.LogInformation($"Updating game item with id: {gameItem.ItemId}");
            var existingItem = _context.GameItems.Find(gameItem.ItemId);
            if(existingItem != null)
            {
                existingItem.ItemName = gameItem.ItemName;
                existingItem.GameName = gameItem.GameName;
                existingItem.Category = gameItem.Category;
                existingItem.Image  = gameItem.Image;
                existingItem.Price = gameItem.Price;
                existingItem.UserId = gameItem.UserId;
                existingItem.Amount = gameItem.Amount;
                _context.SaveChanges();
            }

            return gameItem;
        }

        public void Delete(int gameId)
        {
            _logger.LogInformation($"Deleting game item with id: {gameId}");
            var gameItem = _context.GameItems.Find(gameId);
            if (gameItem != null)
            {
                _context.GameItems.Remove(gameItem);
                _context.SaveChanges();
            }
        }
    }
}