using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Product.Repositories.Products;

namespace OnShop.DAL.Product.Repositories.Products
{
    public class ProductsRepository : EfRepository<Domain.Product.Entities.Product>, IProductsRepository
    {
        public ProductsRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IReadOnlyList<Domain.Product.Entities.Product>> GetPagedRespondAsync(ISpecification<Domain.Product.Entities.Product> spec, int pageSize = 10, int skip = 0, int? pageNumber = null,
            bool asNoTracking = true)
        {
            if (pageNumber.HasValue)
                skip = (pageNumber.Value - 1) * pageSize;
            if (pageSize == 0)
                pageSize = 10;
            var specificationResult = ApplySpecification(spec).Skip(skip)
                .Take(pageSize);
            if (asNoTracking)
                specificationResult = specificationResult.AsNoTracking();
            specificationResult = specificationResult.Include(x => x.Category).Include(x => x.ProductImages).Include(x => x.Brand);
            return await specificationResult.ToListAsync();
        }

        public async Task<bool> IsUniqueProductCodeAsync(string code)
        {
            return await DbContext.Products.Where(x => x.IsRemoved).AllAsync(x => x.Code.ToLower() != code.ToLower());
        }
    }
}