using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnShop.DAL.Slider.Configs
{
    public class SliderConfig : IEntityTypeConfiguration<Domain.Slider.Entities.Slider>
    {
        public void Configure(EntityTypeBuilder<Domain.Slider.Entities.Slider> builder)
        {
            builder.Property(x => x.Title).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired();
            builder.Property(x => x.LinkTitle).HasColumnType("nvarchar(200)").HasMaxLength(200);
            builder.Property(x => x.Description).HasColumnType("nvarchar(1000)").HasMaxLength(1000);
            builder.Property(x => x.Link).HasColumnType("varchar(500)");
            builder.Property(x => x.Src).HasColumnType("varchar(1000)").HasMaxLength(1000).IsRequired();
            

        }
    }
}