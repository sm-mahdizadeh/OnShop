using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.User.Entities;
using OnShop.Web.Common;
using System.Threading.Tasks;

namespace OnShop.Web.Areas.Admin.Controllers
{

    [Area(Const.Area.Admin)]
    [Authorize]
    public class HomeController : BaseController<ApplicationUser>
    {

        public HomeController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
        {
        }
        public async Task< IActionResult> Index()
        {
            if ((await CurrentUser()).StoreId == null)
            {
                return RedirectToAction("Create", "Store", new { area = Const.Area.Admin });
            }
            return View();
        }
        public IActionResult UserManagment()
        {
            return View();
        }
        public IActionResult ProductManagment()
        {
            return View();
        }
        public IActionResult SiteManagment()
        {
            return View();
        }
        public IActionResult Blogmanagment()
        {
            return View();
        }
        public IActionResult Others()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }
    }
}
