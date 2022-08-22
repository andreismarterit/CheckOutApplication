using CheckOut.DataAccess;
using CheckOut.DataAccess.Entities.Baskets;
using CheckOut.Infrastructure.DesignServices;
using Microsoft.EntityFrameworkCore;

namespace CheckOut.BusinsessLogic.DesingServices.Loaders
{
    public interface IBasketByIdLoader : ILoaderService<Basket, Guid>
    {
    }

    internal class BasketByIdLoader : IBasketByIdLoader
    {
        private readonly CheckOutDbContext _checkOutDbContext;

        public BasketByIdLoader(CheckOutDbContext checkOutDbContext)
        {
            _checkOutDbContext = checkOutDbContext;
        }

        public async Task<Basket> LoadAsync(Guid id)
        {
            return await _checkOutDbContext.Baskets.Include(x => x.Items).FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
