namespace CheckOut.BusinsessLogic.WebApiCommands.Baskets.AddArticle.Dtos
{
    public class BasketAddArticleFromBodyRequestDto
    {
        public string Item { get; set; }

        public decimal Price { get; set; }
    }
}