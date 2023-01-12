using OnShop.Domain.DTOs;

namespace OnShop.Domain.Stores.Dtos
{
    public class StoreDto : BaseModelDto<long>
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StoreType { get; set; }
        public string StoreTypeName { get; set; }
        public int MembershipType { get; set; }
        public string MembershipTypeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BigLogo { get; set; }
        public string SmallLogo { get; set; }
    }
}