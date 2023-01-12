using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Domain.Product.Dtos.Brands;
using OnShop.Domain.Product.Queries.Brands;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Common.Interfaces;
using OnShop.Web.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class BrandController : BaseController<ApplicationUser>
    {
        private readonly string brandFilePath = @"file\images\brands";
        private readonly IFileHandler _fileHandler;
        private readonly IMapper _mapper;


        public BrandController(IMediator mediator, UserManager<ApplicationUser> userManager, IFileHandler fileHandler, IMapper mapper) : base(mediator, userManager)
        {
            _fileHandler = fileHandler;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            //var cmd = await Mediator.Send(new GetBrandQueries());
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

            var cmd = await Mediator.Send(new GetBrandQueries { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });
            var recordsTotal = cmd?.FirstOrDefault()?.Count;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cmd };
            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateBrandDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandDto model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.File != null)
            {
                var file = await _fileHandler.UploadFileAsync(model.File, brandFilePath);
                model.Src = file.Status ? file.FileNameAddress : null;
            }
            var command = new BrandCreateCommand()
            {
                Description = model.Description,
                EnglishTitle = model.EnglishTitle,
                Src = model.Src,
                Title = model.Title
            };
            var res = await Mediator.Send(command);
            if (res.IsSuccess)
                return RedirectToAction(nameof(Index));
            foreach (var itemError in res.Errors)
                ModelState.AddModelError("", itemError);
            return View(model);

        }


        public async Task<IActionResult> Edit(int id)
        {
            var cmd = await Mediator.Send(new GetBrandByIdQueries { Id = id });
            var editBrand = _mapper.Map<GetBrandDto, EditBrandDto>(cmd);
            if (cmd != null)
                return View(editBrand);
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBrandDto editModel)
        {
            if (!ModelState.IsValid) return View(editModel);
            if (editModel != null)
            {
                if (editModel.File != null && editModel.File.Length > 0)
                {
                    var file = await _fileHandler.UploadFileAsync(editModel.File, brandFilePath);
                    editModel.Src = file.Status ? file.FileNameAddress : string.Empty;
                }
                var editBrand = _mapper.Map<EditBrandDto, BrandUpdateCommand>(editModel);
                var result = await Mediator.Send(editBrand);
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
                return View(editModel);
            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await Mediator.Send(new DeleteBrandByIdCommand() { Id = id });
            return Json(res);
        }
    }
}
