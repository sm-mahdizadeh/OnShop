using Microsoft.AspNetCore.Mvc;
using OnShop.ApplicationServices.Services.Interface;
using System.Threading.Tasks;
using MediatR;
using OnShop.Domain.Slider.Queries;

namespace OnShop.Web.ViewComponents
{
    [ViewComponent(Name = "Slider")]
    public class Slider : ViewComponent
    {
        private readonly IMediator _mediator;

        public Slider(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IViewComponentResult Invoke()
        {
            var res = _mediator.Send(new SliderPaginationQueries() { PageSize = 4, Skip = 0 }).Result;
            return View("Slider", res.Data);
        }
    }
}
