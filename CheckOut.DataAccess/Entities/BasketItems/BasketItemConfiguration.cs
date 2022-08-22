using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CheckOut.DataAccess.Entities.BasketItems
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.BasketID);
            builder.Property(p => p.Item);
            builder.Property(p => p.Price).HasPrecision(18, 6);

            builder.HasOne(p => p.Basket).WithMany(x => x.Items).HasForeignKey(p => p.BasketID);
        }
    }
}
