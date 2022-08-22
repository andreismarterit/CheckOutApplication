namespace CheckOut.BusinsessLogic.WebApiCommands.Baskets.GetById.Dtos
{
    public class BasketGetByIdResponseDto
    {
        public Guid ID { get; set; }

        public List<BasketGetByIdItemResponseDto> Items { get; set; }

        public decimal TotalNet { get; set; }

        public decimal TotalGross { get; set; }

        public string Customer { get; set; }

        public bool PaysVAT { get; set; }
    }

    public class BasketGetByIdItemResponseDto
    {
        public string Item { get; set; }

        public decimal Price { get; set; }
    }
}
