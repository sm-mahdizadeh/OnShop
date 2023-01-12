using MediatR;
using OnShop.Domain.Enum;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Arrangements.Commands
{
    public class AddArrangementCommand : BaseCommandEntity, IRequest<ResultDto>
    {
        public long StoreId { get; set; }
        public ArrangementItems Type { get; set; }
        public DisplayPriority Priority { get; set; }
        public long? TargetId { get; set; }
        public string Description { get; set; }
    }
}