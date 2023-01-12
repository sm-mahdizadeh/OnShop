using System;
using System.Linq.Expressions;
using Ardalis.Specification;
using JetBrains.Annotations;
using OnShop.Domain.Enum;

namespace OnShop.ApplicationServices.Specifications.Slider
{
    public sealed class SliderSpecification : Specification<Domain.Slider.Entities.Slider>
    {
        public SliderSpecification(string searchKey, [CanBeNull] Expression<Func<Domain.Slider.Entities.Slider, object>> filter = null, OrderType sortDirection = OrderType.Ascending)
        {
            Query.OrderByDescending(x => x.Id);
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