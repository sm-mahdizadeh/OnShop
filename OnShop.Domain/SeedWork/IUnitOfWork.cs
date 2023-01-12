using System.Threading;
using System.Threading.Tasks;

namespace OnShop.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));

        int Commit();

    }
}