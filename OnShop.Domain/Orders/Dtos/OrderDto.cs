using OnShop.Domain.DTOs;
using System;
using System.Collections.Generic;

namespace OnShop.Domain.Orders.Dtos
{
    public class OrderDto : BaseModelDto<long>
    {
        public decimal Amount { get; set; }
        public string Factor => $"ZKD-{Id}";
        public DateTime OrderDate { get; set; }
        public string OrderState { get; set; }
        public List<OrderDetailsDto> OrderDetails { get; set; }
    }

    public class OrderDetailsDto
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public string CategoryName { get; set; }

    }
}