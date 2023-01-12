using System.Threading.Tasks;
using OnShop.Domain.Interfaces;

namespace OnShop.Domain.Product.Repositories.Products
{
    public interface IProductsRepository : IAsyncRepository<Entities.Product>
    {
        Task<bool> IsUniqueProductCodeAsync(string code);
    }
}