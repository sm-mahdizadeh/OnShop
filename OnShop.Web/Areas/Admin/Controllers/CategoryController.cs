using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Product.Commands.Categories;
using OnShop.Domain.Product.Queries.Categories;
using OnShop.Domain.User.Entities;
using OnShop.Web.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using OnShop.Domain.Product.Dtos.Categories;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class CategoryController : BaseController<ApplicationUser>
    {
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper) : base(mediator, userManager)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            //var cmd = await Mediator.Send(new GetCategoryQueries());
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

            var cmd = await Mediator.Send(new GetCategoryPaginationQueries() { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });

            var jsonData = new { draw = draw, recordsFiltered = cmd.TotalCount, recordsTotal = cmd.TotalCount, data = cmd.Data };
            return Json(jsonData);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var pr = await Mediator.Send(new GetCategoryQueries());
            var model = new CreateCategoryDto
            {
                Parents = pr.ToSelectItemList(e => e.Title, e => e.Id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var command = new CategoryCreateCommand()
            {
                CreateDate = DateTime.Now,
                CreatorUserId = (await CurrentUser()).Id,
                Icon = model.Icon,
                ParentId = model.ParentId > 0 ? model.ParentId : null,
                Title = model.Title

            };
            var res = await Mediator.Send(command);
            if (res.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var res = await Mediator.Send(new DeleteCategoryByIdCommand { Id = id });
            return Json(res);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var res = await Mediator.Send(new GetCategoryByIdQueries() { Id = id });
            if (res.IsSuccess)
            {
                var pr = await Mediator.Send(new GetCategoryQueries());
                var editModel = _mapper.Map<GetCategoryDto, EditCategoryDto>(res.Data);
                if (pr.Any())
                    editModel.Parents = pr.Where(x => x.Id != id).ToSelectItemList(e => e.Title, e => e.Id);
                return View(editModel);
            }
            return View("NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryDto model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model == null) return View("NotFound");
            var map = _mapper.Map<EditCategoryDto, CategoryUpdateCommand>(model);
            map.ModifiedId = (await CurrentUser()).Id;

            var result = await Mediator.Send(map);
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
            var pr = await Mediator.Send(new GetCategoryQueries());
            if (pr.Any())
                model.Parents = pr.Where(x => x.Id != model.Id).ToSelectItemList(e => e.Title, e => e.Id);
            return View(model);

        }
    }
}
