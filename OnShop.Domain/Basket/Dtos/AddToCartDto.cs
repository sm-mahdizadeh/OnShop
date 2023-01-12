using System;

namespace OnShop.Domain.Basket.Dtos
{
    public class CartEventDto
    {
        public long ProductId { get; set; }
        public Guid BrowserId { get; set; }
        public int? UserId { get; set; }
    }
}