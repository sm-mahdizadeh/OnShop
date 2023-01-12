using System.Threading.Tasks;
using OnShop.Domain.Interfaces;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.User.Entities;

namespace OnShop.Domain.Product.Repositories.Brands
{
    public interface IBrandsCommandRepository : IAsyncRepository<Brand>
    {
        Task<bool> IsUniqueBrandAsync(string brand, int? brandId = null);
    }
}