using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Stores.Commands;
using OnShop.Domain.Stores.Queries;
using OnShop.Domain.User.Entities;
using OnShop.Web.Common;
using OnShop.Web.Models;

namespace OnShop.Web.Controllers
{
    public class StoresController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        public StoresController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 12, int? categoryId = null, string categoryName = null, string searchKey = "", int? orderBy = 0)
        {
            var skip = (pageSize * page) - pageSize;
            var model = await _mediator.Send(new StorePaginationQuery { SearchKey = searchKey, PageSize = pageSize, Skip = skip });

            return View(new StoreViewModel
            {
                SearchKey = searchKey,
                Stores = model.Data,
                TotalCount = model.TotalCount
            });
        }

        public async Task<IActionResult> My()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                if ((await _userManager.GetUserAsync(User)).StoreId.HasValue)
                {
                    return RedirectToAction("Index", "Home", new { area = Const.Area.Admin });
                }
                else
                {
                    return RedirectToAction("Register", "Stores");
                }

            }
            else
            {

                return View("LoginInfo");
            }
        }
        public IActionResult Register()
        {

            return View();

        }

        public async Task<IActionResult> Register(StoreRegisterViewModel model)
        {
            var result = await _mediator.Send(new StoreCreateCommand
            {
                Title = model.Title,
                PhoneNumber = model.Phone,
                Description = model.Description,
                Address = model.Address,
                CreatorUserId = (await _userManager.GetUserAsync(User)).Id,

            });
            if (result.IsSuccess)
                return RedirectToAction("Index", "Home", new { area = Const.Area.Admin });

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err);
            }
            return View( model);
        }
        public IActionResult Terms()
        {

            return View();

        }
    }
}
