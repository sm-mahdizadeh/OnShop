using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Blogs.Commands.Posts;
using OnShop.Domain.Blogs.Queries.Posts;
using OnShop.Domain.Product.Commands.Products;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Common.Interfaces;
using OnShop.Web.Areas.Admin.Models;
using OnShop.Web.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using OnShop.Domain.Blogs.Queries.PostCategories;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class PostController : BaseController<ApplicationUser>
    {
        private readonly string PostUploadFilePath = @"upload\post";
        private readonly IFileHandler _fileHandler;
        public PostController(IMediator mediator, UserManager<ApplicationUser> userManager, IFileHandler fileHandler) : base(mediator, userManager)
        {
            _fileHandler = fileHandler;
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

            var cmd = await Mediator.Send(new PostPaginationQueries() { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });

            var jsonData = new { draw = draw, recordsFiltered = cmd.TotalCount, recordsTotal = cmd.TotalCount, data = cmd.Data };
            return Json(jsonData);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await Mediator.Send(new PostCategoryListQuery());
            return View(new PostViewModel
            {
                Categories = categories.Data.ToSelectItemList(e => e.Title, e => e.Id),
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.File != null)
            {
                var file = await _fileHandler.UploadFileAsync(model.File, PostUploadFilePath);
                model.ImageSrc = file.Status ? file.FileNameAddress : null;
            }
            var command = new PostCreateCommand
            {

                CreatorUserId = (await CurrentUser()).Id,
                Image = model.ImageSrc,
                CategoryId = model.CategoryId,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                IsActive = model.IsActive,
                Tags = model.Tages,

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
            var model = await Mediator.Send(new PostGetByIdQuery() { Id = id });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model == null) return View("NotFound");

            if (model.File != null)
            {
                var file = await _fileHandler.UploadFileAsync(model.File, PostUploadFilePath);
                model.ImageSrc = file.Status ? file.FileNameAddress : null;
            }
            var result = await Mediator.Send(new PostUpdateCommand
            {
                ModifierUserId = (await CurrentUser()).Id,
                Image = model.ImageSrc,
                CategoryId = model.CategoryId,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                IsActive = model.IsActive,
                Tags = model.Tages,
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
        public async Task<IActionResult> Delete(long id)
        {
            var result = await Mediator.Send(new PostDeleteCommand { Id = id,IsSoftDelete = true});
            return Json(result);
        }

    }
}
