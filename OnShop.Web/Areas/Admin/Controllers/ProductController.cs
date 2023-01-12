using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Domain.Product.Commands.Products;
using OnShop.Domain.Product.Queries.Brands;
using OnShop.Domain.Product.Queries.Categories;
using OnShop.Domain.Product.Queries.Products;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Common.Interfaces;
using OnShop.Web.Areas.Admin.Models;
using OnShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnShop.Domain.Product.Dtos.Product;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize]
    public class ProductController : BaseController<ApplicationUser>
    {
        private readonly IFileHandler _fileHandler;
        private readonly string productImagesFilePath = @"file\images\product";

        public ProductController(IMediator mediator, UserManager<ApplicationUser> userManager, IFileHandler fileHandler) : base(mediator, userManager)
        {
            _fileHandler = fileHandler;
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

            var cmd = await Mediator.Send(new GetProductQueries { PageSize = pageSize, SearchKey = searchValue, Skip = skip, SortColumn = sortColumn, SortDirection = sortColumnDirection });
            var recordsTotal = cmd.TotalRow;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cmd.Products };
            return Json(jsonData);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await Mediator.Send(new GetCategoryQueries());
            var brands = await Mediator.Send(new GetBrandQueries { PageSize = 1000 });
            var model = new ProductViewModel
            {
                ProductDto = new ProductDto()
                {
                    Category = categories.ToSelectItemList(e => e.Title, e => e.Id),
                    Brand = brands.ToSelectItemList(e => e.Title, e => e.Id),
                }
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            var technicalCommand = new List<ProductTechnicalCommand>();
            var featuresCommand = new List<ProductFeaturesCommand>();
            var productImages = new List<ProductImagesCommand>();
            if (!string.IsNullOrEmpty(product.ProductFeatureDto))
            {
                featuresCommand = JsonConvert.DeserializeObject<List<ProductFeaturesCommand>>(product.ProductFeatureDto);
            }
            if (!string.IsNullOrEmpty(product.ProductTechnical))
            {
                technicalCommand = JsonConvert.DeserializeObject<List<ProductTechnicalCommand>>(product.ProductTechnical);
            }
            var files = Request.Form.Files;

            if (files.Any())
                ModelState.Remove(nameof(ProductDto.Files));

            foreach (var images in files)
            {
                var file = await _fileHandler.UploadFileAsync(images, $"{productImagesFilePath}\\{product.Code}");
                productImages.Add(new ProductImagesCommand() { Src = file.FileNameAddress, IsBaseImage = false, IsShow = true });
            }


            if (ModelState.IsValid)
            {
                var command = new ProductsCreateCommand
                {
                    Title = product.Title,
                    EnglishTitle = product.EnglishTitle,
                    Code = product.Code,

                    Discount = product.Discount,
                    PriceDiscount = product.PriceDiscount,
                    Price = product.Price,

                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId,
                    BrandId = product.BrandId,
                    CanPurchase = product.CanPurchase,
                    Displayed = product.Displayed,
                    Description = product.Description,
                    Tag = product.Tag,
                    ShortDescription = product.ShortDescription,
                    CreatorUserId = (await CurrentUser()).Id,
                    ProductImages = productImages.Any() ? productImages : null,
                    ProductFeatures = featuresCommand.Any() ? featuresCommand : new List<ProductFeaturesCommand>(),
                    ProductTechnician = technicalCommand.Any() ? technicalCommand : new List<ProductTechnicalCommand>()

                };

                var res = await Mediator.Send(command);
                if (res.IsSuccess)
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(res);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (res.Errors.Any())
                    {
                        foreach (var item in res.Errors)
                        {
                            res.Message += item + "\n";
                        }
                        if (Request.IsAjaxRequest())
                        {
                            return Json(res);
                        }
                        return RedirectToAction(nameof(Index), new { area = "admin" });
                    }
                }
            }
            var categories = await Mediator.Send(new GetCategoryQueries());
            var brands = await Mediator.Send(new GetBrandQueries { PageSize = 1000 });

            var model = new ProductViewModel
            {
                ProductDto = new ProductDto()
                {
                    Category = categories.ToSelectItemList(e => e.Title, e => e.Id),
                    Brand = brands.ToSelectItemList(e => e.Title, e => e.Id),
                }
            };

            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var res = await Mediator.Send(new DeleteProductCommand { Id = id });
            return Json(res);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var res = await Mediator.Send(new GetAdminProductByIdQueries { Id = id });
            if (res != null)
            {
                var categories = await Mediator.Send(new GetCategoryQueries());
                var brands = await Mediator.Send(new GetBrandQueries { PageSize = 1000 });
                var model = new ProductDto()
                {
                    Category = categories.ToSelectItemList(e => e.Title, e => e.Id),
                    Brand = brands.ToSelectItemList(e => e.Title, e => e.Id),
                    
                    Price = res.Price,
                    Discount = res.Discount,
                    PriceDiscount = res.PriceDiscount,
                    
                    Title = res.Title,
                    EnglishTitle = res.EnglishTitle,
                    Quantity = res.Quantity,
                    Tag = res.Tag,
                    CanPurchase = res.CanPurchase,
                    Description = res.Description,
                    ShortDescription = res.ShortDescription,
                    BrandId = res.BrandId,
                    CategoryId = res.CategoryId,
                    Code = res.Code,
                };


                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto product,string editor1)
        {
            ModelState.Remove(nameof(ProductDto.Files));
            ModelState.Remove(nameof(ProductDto.Description));
            if (!ModelState.IsValid) return View(product);
            if (product != null)
            {

                var cmdModel = new ProductsUpdateCommand()
                {
                    Id = product.Id,
                    Title = product.Title,
                    EnglishTitle = product.EnglishTitle,
                    Code = product.Code,
                    Discount = product.Discount,
                    PriceDiscount = product.PriceDiscount,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId,
                    BrandId = product.BrandId,
                    CanPurchase = product.CanPurchase,
                    Displayed = product.Displayed,
                    Description = editor1,
                    Tag = product.Tag,
                    ShortDescription = product.ShortDescription,
                    ModifiedId = (await CurrentUser()).Id,

                };

                var res = await Mediator.Send(cmdModel);
                if (res.IsSuccess)
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(res);
                    }
                    return RedirectToAction(nameof(Index),new {area="admin"});

                }
                foreach (var itemError in res.Errors)
                    ModelState.AddModelError("", itemError);
                return View(product);
            }
            return View("NotFound");
        }


    }
}

