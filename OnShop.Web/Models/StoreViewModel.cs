using OnShop.Domain.Blogs.Dtos;
using System.Collections.Generic;
using OnShop.Domain.Stores.Dtos;

namespace OnShop.Web.Models
{
    public class StoreViewModel
    {
        public string SearchKey { get; set; }
        public string Title { get; set; }
        public IEnumerable<StoreDto> Stores { get; set; }
        public int TotalCount { get; set; }
    }
}
