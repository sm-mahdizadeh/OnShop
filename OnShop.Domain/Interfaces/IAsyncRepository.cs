using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace OnShop.Domain.Interfaces
{
    public interface IAsyncRepository<T> : IAggregateRoot
    {
        Task<T> GetByIdAsync<TKey>(TKey id, bool asNoTracking = false);
        Task<IReadOnlyList<T>> ListAllAsync(bool asNoTracking = false);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, bool asNoTracking = false);
        Task AddAsync(T entity);
        Task AddAsync(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(List<T> entity);
        Task<int> CountAsync(ISpecification<T> spec, bool asNoTracking = false);
        Task<T> FirstAsync(ISpecification<T> spec, bool asNoTracking = false);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec, bool asNoTracking = false);
        EntityState SoftDelete(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> RawSql(string query, params object[] parameters);
        Task<IReadOnlyList<T>> GetPagedRespondAsync(int pageSize, int skip = 1, int? pageNumber = null);
        Task<IReadOnlyList<T>> GetPagedRespondAsync(ISpecification<T> spec, int pageSize, int skip = 1, int? pageNumber = null, bool asNoTracking = true);
        IQueryable<T> ApplySpecification(ISpecification<T> spec);

        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}