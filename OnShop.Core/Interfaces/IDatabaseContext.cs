using Microsoft.EntityFrameworkCore;
using OnShop.Domain.User.Entities;
using OnShop.Domain.Wallet.Entities;
using OnShop.Domain.Zone.Entities;

namespace OnShop.ApplicationServices.Interfaces
{
    public interface IDatabaseContext
    {
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserInfo> ApplicationUserInfos { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Domain.Wallet.Entities.Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
    }
}