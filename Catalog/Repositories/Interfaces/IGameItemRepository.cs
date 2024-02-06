using Catalog.Entities;

namespace Catalog.Repositories.Interfaces
{    
    public interface IGameItemRepository
    {
        Task<IEnumerable<GameItem>> GetAll();
        Task<GameItem> GetById(int id);
        Task<GameItem> Create(GameItem gameItem);
        Task<GameItem> Update(GameItem gameItem);
        Task Delete(int ItemId);
    }
}


