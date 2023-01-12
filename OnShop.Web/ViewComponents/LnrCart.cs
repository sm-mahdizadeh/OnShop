using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Basket.Queries;
using OnShop.Domain.User.Entities;
using OnShop.Web.Common;

namespace OnShop.Web.ViewComponents
{
    [ViewComponent(Name = "LnrCart")]
    public class LnrCart : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly CookiesManager _cookies;
        private readonly UserManager<ApplicationUser> _userManager;
        public LnrCart(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _cookies = new CookiesManager();
        }
        public IViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(UserClaimsPrincipal).Result;
            var browserId = _cookies.GetBrowserId(HttpContext);
            var res = _mediator.Send(new GetCountItemQueries { BrowserId = browserId, UserId = user?.Id }).Result;
            return View("LnrCart", res.Data);
        }
    }
}