using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnShop.Domain.DTOs.Role;
using OnShop.Domain.User.Entities;
using System.Linq;
using System.Threading.Tasks;
using OnShop.Web.Common;

namespace OnShop.Web.Areas.Admin.Controllers
{
    [Area(Const.Area.Admin)]
    [Authorize(Roles = Const.Roles.Admin)]
    public class RolesController : BaseController<ApplicationUser>
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        public RolesController(IMediator mediator, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : base(mediator, userManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int? pageSize = null)
        {
            var role = await _roleManager.Roles.AsNoTracking().Select(x => new RoleListDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();

            return View(role);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RolesAddDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolesAddDto model)
        {
            var role = new ApplicationRole()
            {
                Name = model.Name,
                Description = model.Description
            };
            var res = await _roleManager.CreateAsync(role);
            if (res.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var err in res.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }
            return View(model);
        }

    }
}
