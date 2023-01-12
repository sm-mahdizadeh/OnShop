using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Boxed.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnShop.Domain.Product.Queries.Products;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Web;
using OnShop.Domain.DTOs.Site.Products;
using OnShop.Domain.Enum;
using OnShop.Domain.Product.Queries.Categories;
using OnShop.Framework.Common;
using OnShop.Domain.Notifications.Commands;

namespace OnShop.Web.Controllers
{
    public class ProductController : BaseController<ApplicationUser>
    {
        public ProductController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
        {
        }
        [HttpGet("product/{categoryId?}/{categoryName?}", Name = "Index")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 12, int? categoryId = null, string categoryName = null, string searchKey = "", int? orderBy = 0)

        {
            //  var command = new NotificationCreateCommand
            //  {
            //      Title = "ورود به فروشگاه : " ,
            //      TargetUserId =(await CurrentUser()).Id,
            //      Type = Domain.Enum.NotificationType.Warning
            //  };
            //await  Mediator.Send(command);

            if (categoryId.HasValue)
            {
                var category = await Mediator.Send(new GetCategoryByIdQueries { Id = categoryId.GetValueOrDefault() });
                if (category != null)
                {
                    var friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(category.Data.Title);

                    if (!string.Equals(friendlyTitle, categoryName, StringComparison.Ordinal))
                    {
                        return RedirectToRoutePermanent("Index", new { categoryId = categoryId, categoryName = friendlyTitle });
                    }
                }
            }
            
            var skip = pageSize * page - pageSize;
            var model = await Mediator.Send(new GetProductQueries { SearchKey = searchKey, PageSize = pageSize, Skip = skip, CategoryId = categoryId, Ordering =(Ordering)orderBy });
            var results = new PagedResults<ProductSiteDto>
            {
                PageSize = pageSize,
                Data = model.Products,
                RowCount = model.TotalRow,
                CurrentPage = page,
                PageCount = model.TotalRow

            };


            return View(results);
        }

        [HttpGet("product/productdetail/{id}/{title?}", Name = "ProductDetail")]
        public async Task<IActionResult> ProductDetail(long id, string title = null)
        {
            var result = await Mediator.Send(new GetProductByIdQueries { Id = id });
            if (result.Product != null)
            {
                string friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(result.Product.Title);
                if (!string.Equals(friendlyTitle, title, StringComparison.Ordinal))
                {
                    return RedirectToRoutePermanent("ProductDetail", new { id = id, title = friendlyTitle });
                }
                return View(result);
            }
            return View("NotFound");

        }
    }
}
