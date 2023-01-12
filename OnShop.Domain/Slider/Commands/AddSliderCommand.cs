using MediatR;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Slider.Commands
{
    public class AddSliderCommand : BaseCommandEntity, IRequest<ResultDto>
    {
        public string Title { get; set; }
        public string Src { get; set; }
        public string Description { get; set; }
        public string LinkTitle { get; set; }
        public string Link { get; set; }
    }
}