using OnShop.Domain.Enum;

namespace OnShop.Domain.Orders.Dtos
{
    public class PaymentResultDto
    {
        public bool IsPay { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Factor { get; set; }
        public long RefId { get; set; }

        public string Message { get; set; }
    }
}