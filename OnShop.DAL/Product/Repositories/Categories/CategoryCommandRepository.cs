using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.Product.Repositories.Categories;

namespace OnShop.DAL.Product.Repositories.Categories
{
    public class CategoryCommandRepository : EfRepository<Category>, ICategoryCommandRepository
    {
        public CategoryCommandRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IReadOnlyList<Category>> ListAllAsync(bool asNoTracking = false)
        {
            var rep = await _dbSet.Include(x => x.Parent).AsNoTracking().ToListAsync();
            return rep;
        }

        public override async Task<IReadOnlyList<Category>> GetPagedRespondAsync(ISpecification<Category> spec, int pageSize = 10, int skip = 0, int? pageNumber = null,bool asNoTracking = true)
        {
            if (pageNumber.HasValue)
                skip = (pageNumber.Value - 1) * pageSize;
            if (pageSize == 0)
                pageSize = 10;
            var specificationResult = ApplySpecification(spec).Skip(skip)
                .Take(pageSize);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            specificationResult = specificationResult.Include(x => x.Parent);
            return await specificationResult.ToListAsync();
        }
    }
}