using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnShop.Domain.Arrangements.Entities;

namespace OnShop.DAL.Arrangements.Configs
{
    public class ArrangementConfig : IEntityTypeConfiguration<Arrangement>
    {
        public void Configure(EntityTypeBuilder<Arrangement> builder)
        {
            builder.HasOne(x => x.Store).WithOne(x => x.Arrangement).HasForeignKey<Arrangement>(x => x.StoreId);
        }
    }
}