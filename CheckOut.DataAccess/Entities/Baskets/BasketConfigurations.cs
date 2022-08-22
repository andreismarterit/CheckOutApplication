using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CheckOut.DataAccess.Entities.Baskets
{
    public class BasketConfigurations : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.Customer);
            builder.Property(p => p.PaysVAT);
            builder.Property(p => p.Close);
            builder.Property(p => p.Payed);
        }
    }
}
