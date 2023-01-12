namespace OnShop.Domain.Basket.Dtos
{
    public class CartItemDto
    {
        public long CartItemId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Count { get; set; }
        
        public decimal Price { get; set; }
        public decimal PriceDiscount { get; set; }
        public decimal FinalPrice { get; set; }

        public string ImageSrc { get; set; }
        //public decimal TotalCount { get; set; }
    }
}