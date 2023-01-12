using System.Security;
using OnShop.Domain.Interfaces;
using OnShop.Domain.Stores.Entities;
using System.Threading.Tasks;

namespace OnShop.Domain.Stores.Repositories
{
    public interface IStoreRepository : IAsyncRepository<Store>
    {
        Task<Store> GetByCodeAsync(string code);
    }
}