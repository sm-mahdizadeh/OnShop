using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Blogs.Commands.PostCategories;
using OnShop.Domain.Blogs.Queries.PostCategories;
using OnShop.Domain.Blogs.Queries.Posts;
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
    public class PostCategoryController : BaseController<ApplicationUser>
    {

        public PostCategoryController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
        {

        }
        public IActionResult Index()
        {
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

            var cmd = await Mediator.Send(new PostCategoryListQuery() { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });

            var jsonData = new { draw = draw, recordsFiltered = cmd.TotalCount, recordsTotal = cmd.TotalCount, data = cmd.Data };
            return Json(jsonData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var command = new PostCategoryCreateCommand
            {

                CreatorUserId = (await CurrentUser()).Id,
                Title = model.Title,

            };
            var res = await Mediator.Send(command);
            if (res.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var command = await Mediator.Send(new PostCategoryGetByIdQuery { Id = id });
            var model = new PostCategoryViewModel
            {

                Title = command.Data.Title,
                Id = command.Data.Id,

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model == null) return View("NotFound");

            var result = await Mediator.Send(new PostCategoryUpdateCommand
            {
                ModifierUserId = (await CurrentUser()).Id,
                Title = model.Title,
                Id = model.Id,
                
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
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await Mediator.Send(new PostCategoryDeleteCommand { Id = id,ModifierUserId = (await CurrentUser()).Id, });
            return Json(res);

        }

    }
}
