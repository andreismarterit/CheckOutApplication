using CheckOut.DataAccess.Entities.BasketItems;
using CheckOut.DataAccess.Entities.Baskets;
using Microsoft.EntityFrameworkCore;

namespace CheckOut.DataAccess
{
    public class CheckOutDbContext : DbContext
    {
        public CheckOutDbContext()
        {
        }

        public CheckOutDbContext(DbContextOptions<CheckOutDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CheckOutDbContext).Assembly);
        }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
