using System;
using Ardalis.Specification;
using OnShop.Domain.Basket.Commands;
using OnShop.Domain.Basket.Entities;

namespace OnShop.ApplicationServices.Specifications.Basket
{
    public sealed class CartSpecification : Specification<Cart>
    {
        public CartSpecification(Guid? browserId, bool isFinished, int? userId = null)
        {
            Query.Where(x => x.IsFinished == isFinished).Include(x => x.CartItems);
            if (browserId.HasValue && userId.HasValue)
            {
                Query.Where(x => x.BrowserId == browserId || x.CreatorUserId == userId);
            }
            else if (browserId.HasValue)
            {
                Query.Where(x => x.BrowserId == browserId);
            }
            else if (userId.HasValue)
            {
                Query.Where(x => x.CreatorUserId == userId);
            }
        }
        public CartSpecification(Guid? browserId, int? userId = null, bool isNeedInclude = true)
        {
            Query.Where(x => !x.IsFinished);
            if (isNeedInclude)
            {
                Query.Include(x => x.CartItems).ThenInclude(x => x.Product).ThenInclude(x => x.ProductImages);
            }
            else
            {
                Query.Include(x => x.CartItems);
            }
            if (browserId.HasValue && userId.HasValue)
            {
                Query.Where(x => x.BrowserId == browserId || x.CreatorUserId == userId);
            }
            else if (browserId.HasValue)
            {
                Query.Where(x => x.BrowserId == browserId);
            }
            else if (userId.HasValue)
            {
                Query.Where(x => x.CreatorUserId == userId);
            }
            Query.OrderByDescending(x => x.Id);
        }

        public CartSpecification(long id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}