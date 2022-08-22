namespace CheckOut.BusinsessLogic.WebApiCommands.Baskets.Create.Dtos
{
    public class BasketCreateFromBodyRequestDto
    {
        public string Customer { get; set; }

        public bool PaysVAT { get; set; }
    }
}
