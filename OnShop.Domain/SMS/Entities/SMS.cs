using OnShop.Domain.Common;
using OnShop.Domain.Interfaces;

namespace OnShop.Domain.SMS.Entities
{
    public class SMS : BaseUserEntity<long>, IAggregateRoot
    {
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}