using OnShop.Domain.DTOs;
using System.Collections.Generic;

namespace OnShop.Domain.Blogs.Dtos
{
    public class PostGetQueryDto : BaseModelDto<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Tages { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public List<PostListQueryDto> Related { get; set; }
        public List<PostListQueryDto> LastPost { get; set; }
    }
}
