using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnShop.Domain.SeedWork;

namespace OnShop.DAL.Context.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _databaseContext.SaveChangesAsync(cancellationToken);

        }

        public int Commit()
        {
            return _databaseContext.SaveChanges();
        }
    }
}
