using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Notifications.Queries;
using System.Threading.Tasks;

namespace OnShop.Web.Areas.Admin.ViewComponents
{

    [ViewComponent(Name = "Notifications")]
    public class Notifications : ViewComponent
    {
        private readonly IMediator _mediator;

        public Notifications(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cmd = await _mediator.Send(new NotificationUnReadCountQueries ());
            return View(cmd.Data);
        }
    }
}
