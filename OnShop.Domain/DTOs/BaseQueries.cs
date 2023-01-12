using OnShop.Domain.Enum;

namespace OnShop.Domain.DTOs
{
    public class BaseQueries
    {
        public int PageSize { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public string SearchKey { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public OrderType Direction { get; set; }
        public Ordering? Ordering { get; set; }

    }
}