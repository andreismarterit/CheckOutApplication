using CheckOut.Infrastructure.Executors.WebApi.Models;
using System.Net;
using CheckOut.Infrastructure.Executors.WebApi.Command;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.Create.Dtos;
using CheckOut.DataAccess.Entities.Baskets;
using CheckOut.BusinsessLogic.DesingServices.Commands;

namespace CheckOut.BusinsessLogic.WebApiCommands.Baskets.Create
{
    public interface IBasketCreateWebApiCommand : IWebApiCommandService<Guid, BasketCreateFromBodyRequestDto>
    {
    }

    internal class BasketCreateWebApiCommand : IBasketCreateWebApiCommand
    {
        private IBasketCreateCommand _basketCreateCommand;

        public BasketCreateWebApiCommand(IBasketCreateCommand basketCreateCommand)
        {
            _basketCreateCommand = basketCreateCommand;
        }

        public async Task<WebApiCommandResponse<Guid>> ExecuteAsync(BasketCreateFromBodyRequestDto basketCreateFromBodyRequestDto)
        {
            var basket = new Basket
            {
                Customer = basketCreateFromBodyRequestDto.Customer,
                PaysVAT = basketCreateFromBodyRequestDto.PaysVAT
            };

            var result = await _basketCreateCommand.ExecuteAsync(basket);

            return new WebApiCommandResponse<Guid>
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseObject = result
            };
        }
    }
}
