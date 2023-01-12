using System.Linq.Dynamic;
using Ardalis.Specification;
using OnShop.Domain.Product.Entities;

namespace OnShop.ApplicationServices.Specifications.Products
{
    public sealed class BrandSpecification : Specification<Brand>
    {
        public BrandSpecification()
        {

        }
        public BrandSpecification(int brandId)
        {
            Query.Where(x => x.Id == brandId);
        }
        public BrandSpecification( string searchKey, string sortColumn, string sortDirection = "asc")
        {
            var query = Query;
            
            //var param = sortColumn;
            //var propertyInfo = typeof(Brand).GetProperty(param);
            
            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(m => m.Title.Contains(searchKey) || m.EnglishTitle.Contains(searchKey));
            }
            //if (!string.IsNullOrEmpty(sortColumn))
            //{
            //    query = query.OrderBy();

            //}

        }

    }
}