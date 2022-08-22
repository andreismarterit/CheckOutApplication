using CheckOut.DataAccess.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckOut.DataAccess.Entities.BasketItems
{
    public class BasketItem
    {
        public Guid ID { get; set; }

        public Guid BasketID { get; set; }

        public string Item { get; set; }

        public decimal Price { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
