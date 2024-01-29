using System;
using Repositories.Interfaces;
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

        public GameItem Update(GameItem gameItem)
        {
            _context.GameItems.Update(gameItem);
            _context.SaveChanges();
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