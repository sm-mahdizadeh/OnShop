using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnShop.Domain.Stores.Entities;

namespace OnShop.DAL.Stores.Configs
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(x => x.Description).HasColumnType("nvarchar(Max)");
            builder.Property(x => x.Title).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(20)").HasMaxLength(20).IsRequired();
            
        }
    }
}