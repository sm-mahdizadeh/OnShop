using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Stores.Commands
{
    public class StoreUpdateCommand : StoreCreateCommand, IRequest<ResultDto>
    {
        public long Id { get; set; }
        public string Code { get; set; }
    }
}