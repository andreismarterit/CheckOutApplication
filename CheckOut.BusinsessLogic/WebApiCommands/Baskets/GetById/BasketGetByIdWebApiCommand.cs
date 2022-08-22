using CheckOut.BusinsessLogic.DesignServices.Builders;
using CheckOut.BusinsessLogic.DesingServices.Loaders;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.GetById.Dtos;
using CheckOut.Infrastructure.Executors.WebApi.Command;
using CheckOut.Infrastructure.Executors.WebApi.Models;
using System.Net;

namespace CheckOut.BusinsessLogic.WebApiCommands.Baskets.GetById
{
    public interface IBasketGetByIdWebApiCommand : IWebApiCommandService<BasketGetByIdResponseDto, Guid>
    {
    }

    internal class BasketGetListWebApiCommand : IBasketGetByIdWebApiCommand
    {
        private readonly IBasketByIdLoader _basketByIdLoader;
        private readonly IBasketGetByResponseDtoBuilder _basketGetByResponseDtoBuilder;

        public BasketGetListWebApiCommand(
            IBasketByIdLoader basketByIdLoader,
            IBasketGetByResponseDtoBuilder basketGetByResponseDtoBuilder)
        {
            _basketByIdLoader = basketByIdLoader;
            _basketGetByResponseDtoBuilder = basketGetByResponseDtoBuilder;
        }

        public async Task<WebApiCommandResponse<BasketGetByIdResponseDto>> ExecuteAsync(Guid id)
        {
            var basket = await _basketByIdLoader.LoadAsync(id);

            if (basket == null)
            {
                return new WebApiCommandResponse<BasketGetByIdResponseDto>
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new List<WebApiCommandValidationError>
                    {
                        new WebApiCommandValidationError(nameof(id), "Not found")
                    }
                };
            }

            return new WebApiCommandResponse<BasketGetByIdResponseDto>
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseObject = await _basketGetByResponseDtoBuilder.BuildAsync(basket)
            };
        }

    }
}
