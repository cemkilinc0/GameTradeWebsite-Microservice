using System;
using Catalog.Repositories.Interfaces;
using Catalog.Entities;
using Catalog.Data;
using Catalog.Events;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class GameItemRepository : IGameItemRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;
        private readonly ILogger<GameItemRepository> _logger;

        public GameItemRepository(CatalogDbContext context, IRabbitMQPublisher publisher, ILogger<GameItemRepository> logger)
        {
            _rabbitMQPublisher = publisher;
            _context = context;
            _logger = logger;
        }

        public async Task<GameItem> Create(GameItem gameItem)
        {
            _logger.LogInformation($"GameItem with id. {gameItem.ItemId} getting created");
            _context.GameItems.Add(gameItem);
            await _context.SaveChangesAsync();
            return gameItem;
        }

        public async Task<IEnumerable<GameItem>> GetAll()
        {
            _logger.LogInformation("Getting all game items");
            return await _context.GameItems.ToListAsync();
        }

        public async Task<GameItem> GetById(int id)
        {
            _logger.LogInformation($"Fetching game item with id: {id}");
            return await _context.GameItems.FirstOrDefaultAsync(item => item.ItemId == id);
        }

        public async Task<GameItem> Update(GameItem gameItem)
        {
            _logger.LogInformation($"Updating game item with id: {gameItem.ItemId}");
            var existingItem = await _context.GameItems.FindAsync(gameItem.ItemId);
            if(existingItem != null)
            {
                existingItem.ItemName = gameItem.ItemName;
                existingItem.GameName = gameItem.GameName;
                existingItem.Category = gameItem.Category;
                existingItem.ImageURL  = gameItem.ImageURL;
                existingItem.Price = gameItem.Price;
                existingItem.OwnerUserId = gameItem.OwnerUserId;
                existingItem.Price = gameItem.Price;
                await _context.SaveChangesAsync();
            }

            _rabbitMQPublisher.PublishItemUpdate(gameItem);

            return existingItem;
        }

        public async Task Delete(int gameId)
        {
            _logger.LogInformation($"Deleting game item with id: {gameId}");
            var gameItem = await _context.GameItems.FindAsync(gameId);
            if (gameItem != null)
            {
                _context.GameItems.Remove(gameItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}