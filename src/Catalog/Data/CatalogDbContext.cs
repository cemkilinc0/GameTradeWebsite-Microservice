using Microsoft.EntityFrameworkCore;
using Catalog.Entities;

namespace Catalog.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<GameItem> GameItems { get; set; }
    }
}