using System.Collections.Generic;
using OnShop.Domain.Enum;

namespace OnShop.Domain.Basket.Dtos
{
    public class CartDto
    {
        public long CartId { get; set; }
        public List<CartItemDto> CartItemDtos { get; set; }
    }
    public class CartPayDto
    {
        public long CartId { get; set; }
        public decimal FinalAmount { get; set; }
        public int Count { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}