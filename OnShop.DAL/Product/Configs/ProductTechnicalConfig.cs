using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnShop.Domain.Product.Entities;

namespace OnShop.DAL.Product.Configs
{
    public class ProductTechnicalConfig : IEntityTypeConfiguration<ProductTechnical>
    {
        public void Configure(EntityTypeBuilder<ProductTechnical> builder)
        {
            builder.Property(x => x.Title).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar(1500)").HasMaxLength(1500).IsRequired();
        }
    }
}