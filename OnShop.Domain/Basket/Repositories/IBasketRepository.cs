using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using OnShop.Domain.Basket.Entities;
using OnShop.Domain.Interfaces;

namespace OnShop.Domain.Basket.Repositories
{
    public interface IBasketRepository : IAsyncRepository<Cart>
    {
        Task AddCartItemAsync(CartItem entity);
        Task AddCartItemAsync(List<CartItem> entity);
        Task<CartItem> GetCartItemAsync(ISpecification<CartItem> spec, bool asNoTracking = false);
        EntityState SoftDeleteCartItem(CartItem entity);
        void DeleteCartItem(CartItem entity);
        Task<CartItem> GetCartItemByIdAsync(long id, bool asNoTracking = false);
        Task<int> CountCartItemAsync(ISpecification<CartItem> spec, bool asNoTracking = false);
    }
}