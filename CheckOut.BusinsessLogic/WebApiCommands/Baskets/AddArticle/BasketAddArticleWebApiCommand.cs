using CheckOut.BusinsessLogic.DesingServices.Loaders;
using CheckOut.BusinsessLogic.WebApiCommands.Baskets.AddArticle.Dtos;
using CheckOut.DataAccess;
using CheckOut.DataAccess.Entities.BasketItems;
using CheckOut.Infrastructure.Executors.WebApi.Command;
using CheckOut.Infrastructure.Executors.WebApi.Models;
using System.Net;

namespace CheckOut.BusinsessLogic.WebApiCommands.Baskets.AddArticle
{
    public interface IBasketAddArticleWebApiCommand : IWebApiCommandService<bool, Guid, BasketAddArticleFromBodyRequestDto>
    {
    }

    public class BasketAddArticleWebApiCommand : IBasketAddArticleWebApiCommand
    {
        private CheckOutDbContext _checkOutDbContext;
        private IBasketByIdLoader _basketByIdLoader;

        public BasketAddArticleWebApiCommand(
            CheckOutDbContext checkOutDbContext,
            IBasketByIdLoader basketByIdLoader)
        {
            _checkOutDbContext = checkOutDbContext;
            _basketByIdLoader = basketByIdLoader;
        }

        public async Task<WebApiCommandResponse<bool>> ExecuteAsync(Guid id, BasketAddArticleFromBodyRequestDto basketAddArticleFromBodyRequestDto)
        {
            var basket = await _basketByIdLoader.LoadAsync(id);

            if (basket == null)
            {
                return new WebApiCommandResponse<bool>
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new List<WebApiCommandValidationError>
                    {
                        new WebApiCommandValidationError(nameof(id), "Not found")
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

            basket.Items.Add(new BasketItem
            {
                Item = basketAddArticleFromBodyRequestDto.Item,
                Price = basketAddArticleFromBodyRequestDto.Price
            });

            await _checkOutDbContext.SaveChangesAsync();

            return new WebApiCommandResponse<bool>
            {
                HttpStatusCode = HttpStatusCode.OK,
                ResponseObject = true
            };
        }
    }
}
