using Microsoft.AspNetCore.Mvc;
using OnShop.Web.Common;

namespace OnShop.Web.Areas.Panel.Controllers
{
    [Area(Const.Area.Panel)]
    //[Route("[area]/[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
