using System;
using System.Linq.Expressions;
using Ardalis.Specification;
using JetBrains.Annotations;
using OnShop.Domain.Enum;
using OnShop.Domain.Product.Entities;

namespace OnShop.ApplicationServices.Specifications.Products
{
    public sealed class CategorySpecification : Specification<Category>
    {
        public CategorySpecification()
        {
            Query.Where(x => x.Parent == null).Include(x => x.Children).ThenInclude(x=>x.Children);
            
        }
        public CategorySpecification(long id)
        {
            Query.Where(x => x.Id == id).Include(x => x.Parent);
        }
        public CategorySpecification(string searchKey, [CanBeNull] Expression<Func<Category, object>> filter = null, OrderType sortDirection = OrderType.Ascending)
        {
            var query = Query;

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(m => m.Title.Contains(searchKey));
            }

            if (filter != null)
            {
                if (sortDirection == OrderType.Ascending)
                {
                    query.OrderBy(filter!);
                }
                else if (sortDirection == OrderType.Descending)
                {
                    query.OrderByDescending(filter);
                }
            }

        }
    }
}