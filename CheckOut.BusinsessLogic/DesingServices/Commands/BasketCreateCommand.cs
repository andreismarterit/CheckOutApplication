using CheckOut.DataAccess.Entities.Baskets;
using CheckOut.DataAccess;
using CheckOut.Infrastructure.DesignServices;

namespace CheckOut.BusinsessLogic.DesingServices.Commands
{
    public interface IBasketCreateCommand : ICommandService<Guid, Basket>
    {
    }

    internal class BasketCreateCommand : IBasketCreateCommand
    {
        private readonly CheckOutDbContext _checkOutDbContext;

        public BasketCreateCommand(CheckOutDbContext checkOutDbContext)
        {
            _checkOutDbContext = checkOutDbContext;
        }

        public async Task<Guid> ExecuteAsync(Basket basket)
        {
            await _checkOutDbContext.AddAsync(basket);
            await _checkOutDbContext.SaveChangesAsync();

            return basket.ID;
        }
    }
}
