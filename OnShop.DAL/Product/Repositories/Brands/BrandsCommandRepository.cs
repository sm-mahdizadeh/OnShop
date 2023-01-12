using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnShop.DAL.Context;
using OnShop.DAL.Data;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.Product.Repositories.Brands;

namespace OnShop.DAL.Product.Repositories.Brands
{
    public class BrandsCommandRepository : EfRepository<Brand>, IBrandsCommandRepository
    {
        public BrandsCommandRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsUniqueBrandAsync(string brand, int? brandId = null)
        {
            if (brandId.HasValue)
            {
                return await DbContext.Brands.AnyAsync(x => x.Id != brandId && x.Title.ToLower() == brand.ToLower());
            }
            return await DbContext.Brands.AnyAsync(x => x.Title.ToLower() == brand.ToLower());
        }
    }
}