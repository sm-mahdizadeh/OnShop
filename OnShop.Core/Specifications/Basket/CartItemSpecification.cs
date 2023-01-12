using System;
using Ardalis.Specification;
using OnShop.Domain.Basket.Entities;

namespace OnShop.ApplicationServices.Specifications.Basket
{
    public sealed class CartItemSpecification : Specification<CartItem>
    {
        public CartItemSpecification(long productId, long cartId)
        {
            Query.Where(x => x.ProductId == productId && x.CartId == cartId);
        }
        public CartItemSpecification(Guid? browserId = null, int? userId = null)
        {
            Query.Where(x => !x.Cart.IsFinished && !x.IsRemoved).Include(x => x.Cart);

            if (browserId.HasValue && userId.HasValue)
            {
                Query.Where(x => x.Cart.BrowserId == browserId || x.CreatorUserId == userId);
            }
            else if (browserId.HasValue)
            {
                Query.Where(x => x.Cart.BrowserId == browserId);
            }
            else if (userId.HasValue)
            {
                Query.Where(x => x.CreatorUserId == userId);
            }
        }

        public CartItemSpecification(long cartItemId, Guid? browserId = null, int? userId = null)
        {
            Query.Where(x => x.Id == cartItemId && !x.Cart.IsFinished).Include(x => x.Cart);
            if (browserId.HasValue && userId.HasValue)
            {
                Query.Where(x => x.Cart.BrowserId == browserId || x.CreatorUserId == userId);
            }
            else if (browserId.HasValue)
            {
                Query.Where(x => x.Cart.BrowserId == browserId);
            }
            else if (userId.HasValue)
            {
                Query.Where(x => x.CreatorUserId == userId);
            }
        }
    }
}