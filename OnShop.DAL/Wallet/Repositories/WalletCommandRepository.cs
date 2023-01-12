using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.Domain.Wallet.Repositories;

namespace OnShop.DAL.Wallet.Repositories
{
    public class WalletCommandRepository : IWalletCommandRepository
    {

        private readonly DatabaseContext _dbContext;

        public WalletCommandRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}