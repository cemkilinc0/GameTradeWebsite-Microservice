using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class GameItemRepository : DbContext
    {
        public GameItemRepository(DbContextOptions<GameItemRepository> options) : base(options)
        {
        }

        public DbSet<GameItem> GameItems { get; set; }
    }
}

