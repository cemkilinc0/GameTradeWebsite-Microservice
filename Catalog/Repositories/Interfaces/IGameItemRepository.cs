using Catalog.Entities;

namespace Catalog.Repositories.Interfaces
{    
    public interface IGameItemRepository
    {
        IEnumerable<GameItem> GetAll();
        GameItem GetById(int id);
        GameItem Create(GameItem gameItem);
        GameItem Update(GameItem gameItem);
        void Delete(int ItemId);
    }
}


