using System;
using System.Linq.Expressions;
using Ardalis.Specification;
using JetBrains.Annotations;
using OnShop.Domain.Enum;

namespace OnShop.ApplicationServices.Specifications.Blogs
{
    public sealed class PostCategorySpecification : Specification<Domain.Blogs.Entities.PostCategory>
    {
        public PostCategorySpecification(long id)
        {
            Query.Where(x => x.Id == id);
        }
        public PostCategorySpecification(string searchKey, [CanBeNull] Expression<Func<Domain.Blogs.Entities.PostCategory, object>> filter = null, OrderType sortDirection = OrderType.Ascending)
        {
            if (!string.IsNullOrEmpty(searchKey))
            {
                Query.Where(m => m.Title.Contains(searchKey));
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