using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Arrangements.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace OnShop.Web.ViewComponents
{
    [ViewComponent(Name = "GetFirstPage")]
    public class GetFirstPage : ViewComponent
    {
        private readonly IMediator _mediator;

        public GetFirstPage(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(GetFirstPageParam model)
        {
            var cmd = await _mediator.Send(new ArrangementsQueries{StoreId = model?.StoreId });
            cmd = cmd.Where(w => w.Priority != OnShop.Domain.Enum.DisplayPriority.Hide).OrderBy(o => o.Priority).ToList();
            return View(cmd);
        }
    }

    public class GetFirstPageParam
    {
        public long? StoreId { get; set; }
    }
}
