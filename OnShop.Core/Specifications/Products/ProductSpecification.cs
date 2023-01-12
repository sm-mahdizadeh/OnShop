using System;
using Ardalis.Specification;
using OnShop.Domain.Enum;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.Product.Queries.Products;

namespace OnShop.ApplicationServices.Specifications.Products
{
    public sealed class ProductSpecification : Specification<Product>
    {
        public ProductSpecification(long productId)
        {
            Query.Where(x => x.Id == productId && !x.IsRemoved);
            Query.Include(x => x.ProductFeatures);
            Query.Include(x => x.ProductImages);
            Query.Include(x => x.ProductTechnicals);
            Query.Include(x => x.Category);
            Query.Include(x => x.Brand);
        }
        public ProductSpecification(GetAdminProductByIdQueries product)
        {
            Query.Where(x => x.Id == product.Id && !x.IsRemoved);
        }


        public ProductSpecification(string searchKey, string sortColumn, OrderType orderType = OrderType.Ascending, Ordering? order = null, long? categoryId = null)
        {
            Query.Where(x => !x.IsRemoved);

            if (!string.IsNullOrEmpty(searchKey))
            {
                Query.Where(m => m.Title.Contains(searchKey) || m.EnglishTitle.Contains(searchKey) || m.Brand.Title.Contains(searchKey) || m.Category.Title.Contains(searchKey));
            }

            if (categoryId.HasValue)
            {
                Query.Where(x => x.CategoryId == categoryId);
            }

            switch (order)
            {
                case Ordering.NotOrdered:
                    Query.OrderByDescending(x => x.Id).ThenBy(x => x.Quantity);
                    break;
                case Ordering.MostVisited:
                    Query.OrderByDescending(x => x.Quantity).ThenByDescending(x => x.ViewCount);
                    break;
                case Ordering.BestSelling:
                    Query.OrderByDescending(x => x.Id).ThenByDescending(x => x.Quantity);
                    break;
                case Ordering.MostPopular:
                    Query.OrderByDescending(x => x.Quantity).ThenByDescending(x => x.Id);
                    break;
                case Ordering.TheNewest:
                    Query.OrderByDescending(x => x.Quantity).ThenByDescending(x => x.Id);
                    break;
                case Ordering.Cheapest:
                    Query.OrderByDescending(x => x.Quantity).ThenBy(x => x.Price);
                    break;
                case Ordering.Expensive:
                    Query.OrderByDescending(x => x.Quantity).ThenByDescending(x => x.Price);
                    break;
                case Ordering.MostStar:
                    Query.OrderByDescending(x => x.Quantity).ThenByDescending(x => x.Id);
                    break;

            }
        }


    }
}