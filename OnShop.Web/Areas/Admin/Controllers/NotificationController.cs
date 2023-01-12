using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Notifications.Commands;
using OnShop.Domain.Notifications.Queries;
using OnShop.Domain.User.Entities;
using OnShop.Web.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class NotificationController : BaseController<ApplicationUser>
    {

        public NotificationController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> List()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;


            var cmd = await Mediator.Send(new NotificationListQuery { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });
            var recordsTotal = cmd.TotalCount;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cmd.Data };
            return Json(jsonData);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var res = await Mediator.Send(new NotificationDeleteCommand { Id = id });
            return Json(res);
        }
        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            var res = await Mediator.Send(new NotificationClearCommand { TargetUserId = (await CurrentUser()).Id });
            return Json(res);
        }
        [HttpPut]
        public async Task<IActionResult> MarkAsRead(long id)
        {
            var res = await Mediator.Send(new NotificationMarkAsReadCommand { Id = id });
            return Json(res);
        }
    }
}
