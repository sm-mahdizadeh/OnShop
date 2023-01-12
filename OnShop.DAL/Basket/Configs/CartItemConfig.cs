using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnShop.Domain.Basket.Entities;

namespace OnShop.DAL.Basket.Configs
{
    public class CartItemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(c => c.Price).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
            builder.Property(c => c.PriceDiscount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
            builder.Property(c => c.FinalPrice).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        }
    }
}
