using OnShop.Domain.DTOs;

namespace OnShop.Domain.Blogs.Dtos
{
    public class PostListQueryDto : BaseModelDto<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string BloggerName { get; set; }

    }
}
