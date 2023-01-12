using System;
using System.Linq.Expressions;
using Ardalis.Specification;
using JetBrains.Annotations;
using OnShop.Domain.Arrangements.Entities;
using OnShop.Domain.Enum;

namespace OnShop.ApplicationServices.Specifications.Arrangements
{
    public sealed class ArrangementsSpecification : Specification<Arrangement>
    {
        public ArrangementsSpecification(long? storeId)
        {
            Query.Where(x =>storeId==null ||  x.StoreId==storeId).Include(x => x.Store);
        }
        
        public ArrangementsSpecification(long id)
        {
            Query.Where(x => x.Id == id).Include(x => x.Store);
        }
        public ArrangementsSpecification(string searchKey, [CanBeNull] Expression<Func<Arrangement, object>> filter = null, OrderType sortDirection = OrderType.Ascending)
        {
            if (!string.IsNullOrEmpty(searchKey))
            {
                Query.Where(m => m.Store.Title.Contains(searchKey));
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