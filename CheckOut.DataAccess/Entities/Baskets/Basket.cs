using CheckOut.DataAccess.Entities.BasketItems;

namespace CheckOut.DataAccess.Entities.Baskets
{
    public class Basket
    {
        public Guid ID { get; set; }

        public string Customer { get; set; }

        public bool PaysVAT { get; set; }

        public bool Close { get; set; }

        public bool Payed { get; set; }

        public virtual ICollection<BasketItem> Items { get; set; }
    }
}
