using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnShop.Domain.Payments.Entities;

namespace OnShop.DAL.Payments.Configs
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(c => c.Amount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.StatusCode).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(x => x.StatusCodeMessage).HasColumnType("nvarchar(200)").HasMaxLength(200);

        }
    }
}