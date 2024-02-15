using Microsoft.EntityFrameworkCore;
using Basket.Entities;

namespace Basket.Data
{
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options)
            : base(options)
        {
        }

        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CustomerBasket> CustomerBaskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names and schemas
            modelBuilder.Entity<BasketItem>().ToTable("BasketItems", "shop");

            // Configure BasketItem relationship
            modelBuilder.Entity<CustomerBasket>()
                .HasMany(b => b.GameItems)
                .WithOne()
                .HasForeignKey(bi => bi.BasketId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            // Configure indexes
            modelBuilder.Entity<BasketItem>().HasIndex(bi => bi.GameItemId);

            // // Configure concurrency tokens
            // modelBuilder.Entity<CustomerBasket>().Property(b => b.Timestamp).IsRowVersion();

            // // Global query filter for soft deletes
            // modelBuilder.Entity<BasketItem>().HasQueryFilter(bi => !bi.IsDeleted);
            // base.OnModelCreating(modelBuilder);
        }

    }
}