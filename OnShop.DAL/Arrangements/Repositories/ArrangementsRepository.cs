using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Arrangements.Entities;
using OnShop.Domain.Arrangements.Repositories;

namespace OnShop.DAL.Arrangements.Repositories
{
    //TODO: Milad: Remove s from class Name
    public class ArrangementsRepository : EfRepository<Arrangement>, IArrangementRepository
    {
        public ArrangementsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}