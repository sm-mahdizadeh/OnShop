using OnShop.Domain.Interfaces;
using OnShop.Domain.Product.Entities;

namespace OnShop.Domain.Product.Repositories.Categories
{
    public interface ICategoryCommandRepository : IAsyncRepository<Category>
    {

    }
}