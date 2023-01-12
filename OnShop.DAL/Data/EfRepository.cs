using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.Domain.Interfaces;
using OnShop.Framework.Domain;

namespace OnShop.DAL.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class, IAggregateRoot
    {
        protected readonly DatabaseContext DbContext;
        protected readonly DbSet<T> _dbSet;

        public EfRepository(DatabaseContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync<TKey>(TKey id, bool asNoTracking = false)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<T>> ListAllAsync(bool asNoTracking = false)
        {
            if (asNoTracking)
                return await _dbSet.AsNoTracking().ToListAsync();
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, bool asNoTracking = false)
        {
            var specificationResult = ApplySpecification(spec);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            return await specificationResult.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(List<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public virtual async Task<int> CountAsync(ISpecification<T> spec, bool asNoTracking = false)
        {
            var specificationResult = ApplySpecification(spec);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            return await specificationResult.CountAsync();
        }

        public virtual async Task<T> FirstAsync(ISpecification<T> spec, bool asNoTracking = false)
        {
            var specificationResult = ApplySpecification(spec);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            return await specificationResult.FirstAsync();
        }

        public virtual async Task<T> FirstOrDefaultAsync(ISpecification<T> spec, bool asNoTracking = false)
        {
            var specificationResult = ApplySpecification(spec);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            return await specificationResult.FirstOrDefaultAsync();
        }

        public virtual EntityState SoftDelete(T entity)
        {
            entity.GetType().GetProperty(nameof(BaseEntity<T>.IsRemoved))?.SetValue(entity, true);
            entity.GetType().GetProperty(nameof(BaseEntity<T>.RemoveTime))?.SetValue(entity, DateTime.Now);
            return _dbSet.Update(entity).State;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual IQueryable<T> RawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters);
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedRespondAsync(ISpecification<T> spec, int pageSize = 10, int skip = 0, int? pageNumber = null, bool asNoTracking = true)
        {
            if (pageNumber.HasValue)
                skip = (pageNumber.Value - 1) * pageSize;
            if (pageSize == 0)
                pageSize = 10;
            var specificationResult = ApplySpecification(spec).Skip(skip)
                .Take(pageSize);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            
            return await specificationResult.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetPagedRespondAsync(int pageSize, int skip = 0, int? pageNumber = null)
        {
            if (pageNumber.HasValue)
                skip = (pageNumber.Value - 1) * pageSize;

            return await _dbSet
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var evaluator = new SpecificationEvaluator<T>();
            return evaluator.GetQuery(_dbSet.AsQueryable(), spec);
        }
        public  async  Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

    }
}