using CheckOut.BusinsessLogic.WebApiCommands.Baskets.CheckOut.Dtos;
using CheckOut.DataAccess;
using CheckOut.Infrastructure.Executors.WebApi.Command;
using CheckOut.Infrastructure.Executors.WebApi.Models;
using System.Net;
using CheckOut.BusinsessLogic.DesingServices.Loaders;

namespace CheckOut.BusinsessLogic.WebApiCommands.Baskets.CheckOut
{
    public interface IBasketCheckOutWebApiCommand : IWebApiCommandService<bool, Guid, BasketCheckOutFromBodyRequest>
    {
    }

    internal class BasketCheckOutWebApiCommand : IBasketCheckOutWebApiCommand
    {
        private CheckOutDbContext _checkOutDbContext;
        private IBasketByIdLoader _basketByIdLoader;

        public BasketCheckOutWebApiCommand(
            CheckOutDbContext checkOutDbContext,
            IBasketByIdLoader basketByIdLoader)
        {
            _checkOutDbContext = checkOutDbContext;
            _basketByIdLoader = basketByIdLoader;
        }

        public async Task<WebApiCommandResponse<bool>> ExecuteAsync(Guid id, BasketCheckOutFromBodyRequest batchCheckOutFromBodyRequest)
        {
            var basket = await _basketByIdLoader.LoadAsync(id);

            if (basket == null)
            {
                return new WebApiCommandResponse<bool>
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new List<WebApiCommandValidationError>
                    {
                        new WebApiCommandValidationError(nameof(id), "Basket not found!")
                    }
                };
            }

            if (basket.Close)
            {
                return new WebApiCommandResponse<bool>
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new List<WebApiCommandValidationError>
                    {
                        new WebApiCommandValidationError(nameof(id), "Basket is closed!")
                    }
                };
            }

            basket.Close = batchCheckOutFromBodyRequest.Close;
            basket.Payed = batchCheckOutFromBodyRequest.Payed;

            await _checkOutDbContext.SaveChangesAsync();

            return new WebApiCommandResponse<bool>
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseObject = true
            };
        }
    }
}
