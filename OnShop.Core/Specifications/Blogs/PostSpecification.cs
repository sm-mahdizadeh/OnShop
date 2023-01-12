using System;
using System.Linq.Expressions;
using Ardalis.Specification;
using JetBrains.Annotations;
using OnShop.Domain.Enum;

namespace OnShop.ApplicationServices.Specifications.Blogs
{
    public sealed class PostSpecification : Specification<Domain.Blogs.Entities.Post>
    {
        public PostSpecification(long id)
        {
            Query.Where(x => x.Id == id).Include(x => x.Category);
            Query.OrderByDescending(x => x.Id);
        }
        public PostSpecification(string searchKey,int? categoryId, [CanBeNull] Expression<Func<Domain.Blogs.Entities.Post, object>> filter = null, OrderType sortDirection = OrderType.Ascending)
        {
            Query.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(searchKey))
            {
                Query.Where(m => m.Title.Contains(searchKey)||m.Description.Contains(searchKey)||m.Content.Contains(searchKey));
            }
            if (categoryId.HasValue)
            {
                Query.Where(m => m.CategoryId==categoryId);
            }
            if (filter != null)
            {
                if (sortDirection == OrderType.Ascending)
                {
                    Query.OrderBy(filter!);
                }
                else if (sortDirection == OrderType.Descending)
                {
                    Query.OrderByDescending(filter);
                }
            }
        }
    }
}