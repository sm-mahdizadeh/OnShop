using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnShop.Web.ViewComponents
{
    [ViewComponent(Name = "Brand")]
    public class Brand : ViewComponent
    {
        private readonly IMediator _mediator;

        public Brand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var cmd = await _mediator.Send(new ArrangementsQueries());
            //cmd = cmd.Where(w => w.Priority != Domain.Enum.DisplayPriority.Hide).OrderBy(o => o.Priority).ToList();
            //return View(cmd);
            return View();
        }
    }
}
