using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.Domain.Enum;
using OnShop.Domain.Wallet.Repositories;

namespace OnShop.DAL.Wallet.Repositories
{
    public class WalletQueryRepository : IWalletQueryRepository
    {
        private readonly DatabaseContext _dbContext;

        public WalletQueryRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> BalanceUserWalletAsync(string userName)
        {
            var debt = await _dbContext.Wallets.Include(x => x.ApplicationUser)
                .Where(x => x.ApplicationUser.UserName == userName && x.WalletTypeId == (int)WalletTypeEnum.Debt
                ).SumAsync(x => x.Amount);

            var demand = await _dbContext.Wallets.Include(x => x.ApplicationUser)
                .Where(x => x.ApplicationUser.UserName == userName && x.WalletTypeId == (int)WalletTypeEnum.Demand).SumAsync(x => x.Amount);

            return (debt - demand);
        }
    }
}