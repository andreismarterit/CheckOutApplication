using CheckOut.BusinsessLogic.WebApiCommands.Baskets.GetById.Dtos;
using CheckOut.DataAccess.Entities.BasketItems;
using CheckOut.DataAccess.Entities.Baskets;
using CheckOut.Infrastructure.DesignServices;

namespace CheckOut.BusinsessLogic.DesignServices.Builders
{
    public interface IBasketGetByResponseDtoBuilder : IBuilderService<BasketGetByIdResponseDto, Basket>
    {
    }

    internal class BasketGetByResponseDtoBuilder : IBasketGetByResponseDtoBuilder
    {
        private const decimal VAT = 19;

        public Task<BasketGetByIdResponseDto> BuildAsync(Basket basket)
        {
            var totalNet = ComputeTotalNet(basket.Items);
            var totalGross = basket.PaysVAT ? ComputeTotalGross(totalNet) : totalNet;

            return Task.FromResult(new BasketGetByIdResponseDto
            {
                ID = basket.ID,
                Customer = basket.Customer,
                PaysVAT = basket.PaysVAT,
                Items = MapItems(basket.Items), // we can use a mapper for this part
                TotalGross = totalGross,
                TotalNet = totalNet
            });
        }

        private decimal ComputeTotalNet(ICollection<BasketItem> items)
        {
            return items.Sum(x => x.Price);
        }

        private decimal ComputeTotalGross(decimal totalNet)
        {
            return totalNet * VAT / 100 + totalNet;
        }

        private List<BasketGetByIdItemResponseDto> MapItems(ICollection<BasketItem> items)
        {
            List<BasketGetByIdItemResponseDto> result = new List<BasketGetByIdItemResponseDto>();
            foreach (var item in items)
            {
                result.Add(new BasketGetByIdItemResponseDto
                {
                    Item = item.Item,
                    Price = item.Price
                });
            }

            return result;
        }
    }
}