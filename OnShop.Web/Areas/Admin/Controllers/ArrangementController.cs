using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Arrangements.Commands;
using OnShop.Domain.Arrangements.Queries;
using OnShop.Domain.User.Entities;
using OnShop.Web.Areas.Admin.Models;
using OnShop.Web.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class ArrangementController : BaseController<ApplicationUser>
    {

        public ArrangementController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
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


            var cmd = await Mediator.Send(new ArrangementsPaginationQueries { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });
            var recordsTotal = cmd.TotalCount;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cmd.Data };
            return Json(jsonData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ArrangmentSave());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ArrangmentSave model)
        {
            if (!ModelState.IsValid) return View(model);

            var command = new AddArrangementCommand
            {
                Description = model.Description,
                Priority = model.Priority,
                Type = model.Type,
                StoreId = 1,
            };
            var res = await Mediator.Send(command);
            if (res.IsSuccess)
                return RedirectToAction(nameof(Index));
            foreach (var itemError in res.Errors)
                ModelState.AddModelError("", itemError);
            return View(model);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var res = await Mediator.Send(new DeleteArrangementCommand { Id = id });
            return Json(res);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var res = await Mediator.Send(new ArrangementByIdQueries() { Id = id });
            if (!res.IsSuccess)
                return View("NotFound");

            return View(new ArrangmentSave
            {
                Description = res.Data.Description,
                Priority = res.Data.Priority,
                Type = res.Data.Type,
                Id = res.Data.Id
            });


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArrangmentSave model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model == null) return View("NotFound");


            var result = await Mediator.Send(new UpdateArrangementCommand
            {
                ModifiedId = (await CurrentUser()).Id,
                Description = model.Description,
                Priority = model.Priority,
                Type = model.Type,
            });
            if (result.IsSuccess)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(result);
                }
                return RedirectToAction(nameof(Index));

            }
            foreach (var itemError in result.Errors)
                ModelState.AddModelError("", itemError);

            return View(model);

        }
    }
}
