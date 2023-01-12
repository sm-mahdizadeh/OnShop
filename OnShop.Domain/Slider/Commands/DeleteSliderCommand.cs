using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Slider.Commands
{
    public class DeleteSliderCommand : IRequest<ResultDto>
    {
        public int Id { get; set; }
        public bool IsSoftDelete { get; set; } = false;
        public int ModifiedId { get; set; }
    }
}