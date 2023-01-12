using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Stores.Entities;
using OnShop.Domain.Stores.Repositories;

namespace OnShop.DAL.Stores.Repositories
{
    public class StoreRepository : EfRepository<Store>, IStoreRepository
    {
        public StoreRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
        public virtual async Task<Store> GetByCodeAsync(string code)
        {
            return await _dbSet.FirstOrDefaultAsync(f=>f.Code==code);
        }

    }
}