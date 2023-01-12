//using Microsoft.EntityFrameworkCore;
//using OnShop.Domain.Wallet.Entities;

//namespace OnShop.DAL.Context
//{
//    public class DatabaseContext : DbContext
//    {
//        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
//        {

//        }

//        public DbSet<Domain.Wallet.Entities.Wallet> Wallets { get; set; }
//        public DbSet<WalletType> WalletTypes { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            base.OnConfiguring(optionsBuilder);
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
//        }
//    }
//}