using System;
using Catalog.Repositories.Interfaces;
using Catalog.Entities;
using Catalog.Data;

namespace Catalog.Repositories
{
    public class GameItemRepository : IGameItemRepository
    {
        private readonly CatalogDbContext _context;

        public GameItemRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public GameItem Create(GameItem gameItem)
        {
            _context.GameItems.Add(gameItem);
            _context.SaveChanges();
            return gameItem;
        }

        public IEnumerable<GameItem> GetAll()
        {
            return _context.GameItems.ToList();
        }

        public GameItem GetById(int id)
        {
            return _context.GameItems.FirstOrDefault(item => item.ItemId == id);
        }

        public GameItem Update(GameItem gameItem)
        {
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
            var gameItem = _context.GameItems.Find(gameId);
            if (gameItem != null)
            {
                _context.GameItems.Remove(gameItem);
                _context.SaveChanges();
            }
        }
    }
}