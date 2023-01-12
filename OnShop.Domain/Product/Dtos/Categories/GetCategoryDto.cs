using OnShop.Domain.DTOs;

namespace OnShop.Domain.Product.Dtos.Categories
{
    public class GetCategoryDto :BaseModelDto<long>
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public string ParentName { get; set; }
        public string Icon { get; set; }
        public GetCategoryDto? Parent { get; set; }

    }
}