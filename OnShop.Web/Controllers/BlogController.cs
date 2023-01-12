using Boxed.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Blogs.Queries.PostCategories;
using OnShop.Domain.Blogs.Queries.Posts;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Common;
using OnShop.Framework.Web;
using OnShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnShop.Web.Controllers
{
    public class BlogController : BaseController<ApplicationUser>
    {
        public BlogController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
        {
        }
        [HttpGet("Blog", Name = "Blog")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 12, int? categoryId = null, string categoryName = null, string key = "")
        {

            var skip = (pageSize * page) - pageSize;
            var model = await Mediator.Send(new PostPaginationQueries { SearchKey = key, PageSize = pageSize, Skip = skip });

            return View(new BlogViewModel
            {
                SearchKey = key,
                Posts = model.Data,
                TotalCount = model.TotalCount
            });

        }
        [HttpGet("Blog/{id}/{title?}", Name = "Post")]
        public async Task<IActionResult> Post(long id, string title)
        {
            var model = await Mediator.Send(new PostGetByIdQuery { Id = id });

            if (model?.Data == null)
                return View("NotFound");


            var friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(model.Data.Title);

            if (!string.Equals(friendlyTitle, title, StringComparison.Ordinal))
            {
                return RedirectToRoutePermanent(nameof(Post), new { id = id, title = friendlyTitle });
            }
            if (model.Data.CategoryId.HasValue)
            {
                var related = await Mediator.Send(new PostPaginationQueries { CategoryId = model.Data.CategoryId, PageSize = 4, Skip = 0 });
                model.Data.Related = related.Data.Where(x => x.Id != id).ToList();
            }
            model.Data.LastPost = (await Mediator.Send(new PostPaginationQueries { CategoryId = model.Data.CategoryId, PageSize = 4, Skip = 0 })).Data.Where(x => x.Id != id).ToList();
            return View(model);
        }

        [HttpGet("Blog/Category", Name = "CategoryList")]
        public async Task<IActionResult> CategoryList()
        {

            var model = await Mediator.Send(new PostCategoryListQuery() { PageSize = 100, SearchKey = "", Skip = 0, SortColumn = "Title" });
            return View("index", new BlogViewModel
            {
                Title = "دسته بندی",
                Categorires = model.Data,
                TotalCount = model.TotalCount
            });

        }

        [HttpGet("Blog/Category/{id}/{title?}", Name = "CategoryPost")]
        public async Task<IActionResult> CategoryPost(int id, string title, int page = 1, int pageSize = 12, int? categoryId = null, string categoryName = null)
        {
            var model = await Mediator.Send(new PostCategoryGetByIdQuery { Id = id });
            if (model == null)
                return View("NotFound");

            var friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(model.Data.Title);

            if (!string.Equals(friendlyTitle, title, StringComparison.Ordinal))
            {
                return RedirectToRoutePermanent(nameof(CategoryPost), new { id = id, title = friendlyTitle });
            }

            var skip = (pageSize * page) - pageSize;
            var list = await Mediator.Send(new PostPaginationQueries { CategoryId = id, PageSize = pageSize, Skip = skip });


            return View("index", new BlogViewModel
            {
                Title = model.Data.Title,
                Posts = list.Data,
                TotalCount = list.TotalCount
            });

        }
        [HttpGet("Blog/Tag/{title?}", Name = "Tag")]
        public async Task<IActionResult> Tag(string title)
        {
            //var model = await Mediator.Send(new PostGetByIdQueries { Id = id });
            //if (model == null)
            //    return View("NotFound");

            //var friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(model.Data.Title);

            //if (!string.Equals(friendlyTitle, title, StringComparison.Ordinal))
            //{
            //    return RedirectToRoutePermanent(nameof(Post), new { id = id, title = friendlyTitle });
            //}
            return View();
        }
    }
}
