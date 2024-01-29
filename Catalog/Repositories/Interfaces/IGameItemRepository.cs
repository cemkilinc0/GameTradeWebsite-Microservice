using Catalog.Entities;

namespace Repositories.Interfaces
{    public interface IGameItemRepository
    {
        GameItem Create(GameItem gameItem);
        GameItem Update(GameItem gameItem);
        void Delete(int ItemId);
    }
}


