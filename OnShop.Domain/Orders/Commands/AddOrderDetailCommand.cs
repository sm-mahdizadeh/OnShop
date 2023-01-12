using OnShop.Domain.Enum;
using OnShop.Framework.Commands;

namespace OnShop.Domain.Orders.Commands
{
    public class AddOrderDetailCommand :BaseCommandEntity
    {

        public long OrderId { get; set; }
        public long ProductId { get; set; }

        public decimal FinalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}