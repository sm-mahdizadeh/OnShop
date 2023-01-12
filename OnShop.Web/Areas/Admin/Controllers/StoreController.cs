using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Common.Interfaces;
using OnShop.Web.Common;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using OnShop.Domain.Stores.Commands;
using OnShop.Domain.Stores.Queries;
using OnShop.Web.Areas.Admin.Models;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class StoreController : BaseController<ApplicationUser>
    {
        private const string StoreUploadFilePath = @"upload\store";
        private readonly IFileHandler _fileHandler;
        public StoreController(IMediator mediator, UserManager<ApplicationUser> userManager, IFileHandler fileHandler) : base(mediator, userManager)
        {
            _fileHandler = fileHandler;
        }
        public IActionResult Index()
        {
            if (!User.HasClaim(ClaimTypes.Role, Const.Roles.Admin))
                return RedirectToAction(nameof(Details));
            return View();
        }
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

            var cmd = await Mediator.Send(new StorePaginationQuery() { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });

            var jsonData = new { draw = draw, recordsFiltered = cmd.TotalCount, recordsTotal = cmd.TotalCount, data = cmd.Data };
            return Json(jsonData);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (!id.HasValue)
                id = (await CurrentUser()).StoreId;
            if (!id.HasValue)
                return  View("NotFound");
            
            var model = await Mediator.Send(new StoreGetByIdQuery() { Id = id.Value });
            if (model==null)
                return View("NotFound");
            var m = new StoreViewModel(model.Data);
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StoreViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model == null) return View("NotFound");

            if (model.BigLogoFile != null)
            {
                var file = await _fileHandler.UploadFileAsync(model.BigLogoFile, StoreUploadFilePath);
                model.BigLogoFileSrc = file.Status ? file.FileNameAddress : null;
            }
            var result = await Mediator.Send(model.Map());
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
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await Mediator.Send(new StoreDeleteCommand { Id = id, IsSoftDelete = true });
            return Json(result);
        }
        public async Task<IActionResult> Details(long? id)
        {
            if (!id.HasValue)
                id = (await CurrentUser()).StoreId;
            if (!id.HasValue)
                return View("NotFound");

            var model = await Mediator.Send(new StoreGetByIdQuery() { Id = id.Value });
            if (model == null)
                return View("NotFound");
            var m = new StoreDetailsViewModel(model.Data);
            return View(m);
        }
    }
}
