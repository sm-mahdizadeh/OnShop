using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnShop.Domain.Orders.Queries;
using OnShop.Domain.User.Commands;
using OnShop.Domain.User.Dtos;
using OnShop.Domain.User.Dtos.UserAddresses;
using OnShop.Domain.User.Entities;
using OnShop.Domain.User.Queries;
using OnShop.Framework.Dtos;
using OnShop.Framework.Web;
using OnShop.Web.Common;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnShop.Web.Areas.Panel.Controllers
{

    [Area(nameof(Panel))]
    //[Route("[area]/[controller]/[action]")]
    [Authorize]
    public class ProfileController : BaseController<ApplicationUser>
    {
        public ProfileController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserInfo()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var _userId = Convert.ToInt16(claim?.Value);
            var model = new ApplicationUserInfoDto() { IsUpdated = false, ApplicationUserId = _userId };
            var cmd = await Mediator.Send(new GetApplicationUserInfoQuery() { ApplicationUserId = _userId });
            if (cmd != null)
            {
                model.FirstName = cmd.FirstName;
                model.LastName = cmd.LastName;
                model.Birthdate = cmd.Birthdate;
                model.ApplicationUserId = cmd.ApplicationUserId;
                model.IsUpdated = true;

                if (cmd.Gender.GetValueOrDefault())
                {
                    model.GenderString = "Male";
                }
                else if (!cmd.Gender.GetValueOrDefault())
                {
                    model.GenderString = "Female";
                }

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserInfo(ApplicationUserInfoDto model)
        {
            bool? gender = null;
            var res = new ResultDto();
            gender = model.GenderString.ToLower() switch
            {
                "male" => true,
                "female" => false,
                _ => null
            };
            if (!ModelState.IsValid) return View(model);
            if (!model.IsUpdated)
            {
                var command = new AddApplicationUserInfoCommand
                {
                    CreateDate = DateTime.Now,
                    CreatorUserId = model.ApplicationUserId,
                    Gender = gender,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Birthdate = model.Birthdate,
                    ApplicationUserId = model.ApplicationUserId,
                    NationalCode = model.NationalCode
                };
                res = await Mediator.Send(command);
            }
            else
            {
                var command = new UpdateApplicationUserInfoCommand
                {
                    CreateDate = DateTime.Now,
                    CreatorUserId = model.ApplicationUserId,
                    Gender = gender,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Birthdate = model.Birthdate,
                    ModifiedId = model.ApplicationUserId
                };
                res = await Mediator.Send(command);
            }
            if (res.IsSuccess)
                return RedirectToAction("Index");

            //ModelState.AddModelError("", result.Message);
            //foreach (var item in result.Errors)
            //{
            //    ModelState.AddModelError("", item);
            //}
            return View(model);
        }


        public async Task<IActionResult> Addresses()
        {
            var userId = (await CurrentUser())?.Id;
            var result = await Mediator.Send(new GetUserAddressQueries() { ApplicationUserId = userId.GetValueOrDefault() });

            return View(result);
        }

        public async Task<IActionResult> AddAddresses()
        {
            var userId = (await CurrentUser())?.Id;
            var zoneList = await GetZones();
            var model = new AddUserAddressesDto() { Zones = zoneList };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddresses(AddUserAddressesDto model)
        {
            if (!ModelState.IsValid)
            {
                model.Zones = await GetZones();
                return View(model);
            }


            var userId = (await CurrentUser())?.Id;
            var command = new AddUserAddressCommand()
            {
                CreatorUserId = userId,
                CreateDate = DateTime.Now,
                ApplicationUserId = userId.GetValueOrDefault(),
                DistrictId = model.DistrictId,
                RecipientIsSelf = true,
                Plaque = model.Plaque,
                PostCode = model.PostCode,
                PostalAddress = model.PostalAddress,
                Unit = model.Unit
            };

            var res = await Mediator.Send(command);
            if (res.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            model.Zones = await GetZones();
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetProvinceList(int zoneId)
        {
            var result = await Mediator.Send(new GetProvinceQueries() { ZoneId = zoneId });
            return Json(result.ToSelectItemList(e => e.Name, e => Convert.ToInt64(e.Value)));

        }
        [HttpGet]
        public async Task<JsonResult> GetDistrictList(int provinceId)
        {
            var result = await Mediator.Send(new GetDistrictQueries() { ProvinceId = provinceId });
            return Json(result.ToSelectItemList(e => e.Name, e => Convert.ToInt64(e.Value)));

        }

        private async Task<List<SelectListItem>> GetZones()
        {
            var zone = await Mediator.Send(new GetZoneQueries());
            return zone.ToSelectItemList(e => e.Name, e => Convert.ToInt64(e.Value));
        }
        [HttpPost]
        public async Task<JsonResult> RemoveAddress(long id)
        {
            var res = await Mediator.Send(new DeleteUserAddressCommand() { Id = id });
            return Json(res);
        }

        public async Task<IActionResult> Orders()
        {
            var userId = (await CurrentUser())?.Id;
            var orders = await Mediator.Send(new GetOrderByUserIdQueries() { UserId = userId.GetValueOrDefault() });
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(long orderId)
        {
            var orders = await Mediator.Send(new GetOrderDetailsQueries() { OrderId = orderId });
            return View(orders);
        }
    }
}
