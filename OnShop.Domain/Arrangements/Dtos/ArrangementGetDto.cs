using OnShop.Domain.DTOs;
using OnShop.Domain.Enum;

namespace OnShop.Domain.Arrangements.Dtos
{
    public class ArrangementGetDto : BaseModelDto<long>
    {
        public long StoreId { get; set; }
        public string StoreIdName { get; set; }
        public ArrangementItems Type { get; set; }
        public DisplayPriority Priority { get; set; }
        public long? TargetId { get; set; }
        public string Description { get; set; }

    }
}