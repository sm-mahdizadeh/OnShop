using System.Threading.Tasks;

namespace OnShop.Domain.Wallet.Repositories
{
    public interface IWalletQueryRepository
    {
        Task<decimal> BalanceUserWalletAsync(string userName);
    }
}