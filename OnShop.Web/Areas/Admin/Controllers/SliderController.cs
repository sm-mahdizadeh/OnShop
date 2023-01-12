using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Product.Queries.Brands;
using OnShop.Domain.Slider.Commands;
using OnShop.Domain.Slider.Dtos;
using OnShop.Domain.Slider.Queries;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Common.Interfaces;
using OnShop.Web.Common;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class SliderController : BaseController<ApplicationUser>
    {
        private readonly string uploadFilePath = @"upload\slider";
        private readonly IFileHandler _fileHandler;
        private readonly IMapper _mapper;

        public SliderController(IMediator mediator, UserManager<ApplicationUser> userManager, IFileHandler fileHandler, IMapper mapper) : base(mediator, userManager)
        {
            _fileHandler = fileHandler;
            _mapper = mapper;
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

            var cmd = await Mediator.Send(new SliderPaginationQueries { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });
            var recordsTotal = cmd.TotalCount;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cmd.Data };
            return Json(jsonData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SliderDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderDto model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.File != null)
            {
                var file = await _fileHandler.UploadFileAsync(model.File, uploadFilePath);
                model.Src = file.Status ? file.FileNameAddress : null;
            }
            var command = new AddSliderCommand()
            {
                Description = model.Description,
                Src = model.Src,
                Title = model.Title,
                CreateDate = DateTime.Now,
                CreatorUserId = (await CurrentUser()).Id,
                LinkTitle = model.LinkTitle,
                IsRemoved = false,
                Link = model.Link,
                
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
            var cmd = await Mediator.Send(new SliderByIdQueries { Id = id });
            var edit = _mapper.Map<SliderDto, EditSliderDto>(cmd.Data);
            if (edit != null)
                return View(edit);
            return View("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditSliderDto editModel)
        {
            if (!ModelState.IsValid) return View(editModel);
            if (editModel != null)
            {
                if (editModel.File != null && editModel.File.Length > 0)
                {
                    var file = await _fileHandler.UploadFileAsync(editModel.File, uploadFilePath);
                    editModel.Src = file.Status ? file.FileNameAddress : string.Empty;
                }
                var editBrand = _mapper.Map<EditSliderDto, UpdateSliderCommand>(editModel);
                editBrand.ModifiedId = (await CurrentUser()).Id;
                editBrand.ModifiedDate=DateTime.Now;
                
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
            var res = await Mediator.Send(new DeleteSliderCommand()
            {
                Id = id,
                IsSoftDelete = true,
                ModifiedId = (await CurrentUser()).Id
            });
            return Json(res);
        }
    }
    
}
