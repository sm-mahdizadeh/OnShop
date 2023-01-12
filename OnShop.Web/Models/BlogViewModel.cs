using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnShop.Domain.Blogs.Dtos;

namespace OnShop.Web.Models
{
    public class BlogViewModel
    {
        public string Title { get; set; }
        public string SearchKey { get; set; } 
        public IEnumerable<PostCategoryListQueryDto> Categorires { get; set; }
        public IEnumerable<PostListQueryDto> Posts { get; set; }
        public int TotalCount { get; set; }
    }
}
