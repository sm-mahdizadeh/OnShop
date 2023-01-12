using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Stores.Dtos;
using OnShop.Domain.Stores.Queries;
using OnShop.Framework.Dtos;
using OnShop.Web.Areas.Store.Models;
using OnShop.Web.Common;
using System.Threading.Tasks;

namespace OnShop.Web.Areas.Store.Controllers
{
    [Area(Const.Area.Store)]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Store/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            ResultDto<StoreDto> result ;
            if (int.TryParse(id, out var storeId))
            {
                result = await _mediator.Send(new StoreGetByIdQuery() { Id = storeId });
            }
            else
            {
                result = await _mediator.Send(new StoreGetByCodeQuery { Code = id });
            }
            return View(new FirstPageViewModel { Store = result.Data });
        }
    }
}
