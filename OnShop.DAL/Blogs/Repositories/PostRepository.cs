using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Blogs.Entities;
using OnShop.Domain.Blogs.Repositories;
using OnShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnShop.DAL.Blogs.Repositories
{
    public class PostRepository : EfRepository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IReadOnlyList<Post>> ListAllAsync(bool asNoTracking = false)
        {
            var rep = await _dbSet.Include(x => x.Category).Include(x=>x.ApplicationUser).AsNoTracking().ToListAsync();
            return rep;
        }

        public override async Task<IReadOnlyList<Post>> GetPagedRespondAsync(ISpecification<Post> spec, int pageSize = 10, int skip = 0, int? pageNumber = null, bool asNoTracking = true)
        {
            if (pageNumber.HasValue)
                skip = (pageNumber.Value - 1) * pageSize;
            if (pageSize == 0)
                pageSize = 10;
            var specificationResult = ApplySpecification(spec).Skip(skip)
                .Take(pageSize);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            specificationResult = specificationResult.Include(x => x.Category);
            return await specificationResult.ToListAsync();
        }



        IQueryable<Post> IAsyncRepository<Post>.RawSql(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Post>> IAsyncRepository<Post>.GetPagedRespondAsync(int pageSize, int skip, int? pageNumber)
        {
            throw new NotImplementedException();
        }


    }
}