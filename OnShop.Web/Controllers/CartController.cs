using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OnShop.Domain.Basket.Commands;
using OnShop.Domain.Basket.Queries;
using OnShop.Domain.DTOs.Site.Products;
using OnShop.Domain.Enum;
using OnShop.Domain.Orders.Commands;
using OnShop.Domain.Orders.Dtos;
using OnShop.Domain.Orders.Queries;
using OnShop.Domain.Payments.Commands;
using OnShop.Domain.User.Entities;
using OnShop.Domain.User.Queries;
using OnShop.Framework.Dtos;
using OnShop.Framework.Rest;
using OnShop.Framework.Web;
using OnShop.Framework.Zarinpal;
using OnShop.Web.Common;
using OnShop.Web.Models;
using TopLearn.Web.Helper;

namespace OnShop.Web.Controllers
{
    public class CartController : BaseController<ApplicationUser>
    {
        private readonly CookiesManager _cookies;
        private readonly string _merchant;
        public CartController(IMediator mediator, UserManager<ApplicationUser> userManager) : base(mediator, userManager)
        {
            _cookies = new CookiesManager();
            _merchant = "43159e03-7b7b-4175-9cc5-5ea5fe546435";
        }
        public async Task<IActionResult> Index()
        {
            var browserId = _cookies.GetBrowserId(HttpContext);
            var userId = (await CurrentUser())?.Id;
            var myCart = await Mediator.Send(new GetCardQueries()
            {
                BrowserId = browserId,
                UserId = userId

            });
            return View(myCart.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(ResultProductDetailsSiteDto model)
        {
            var res = await AddCard(model.ProductAddToCart.ProductId, model.ProductAddToCart.Count);
            if (res.IsSuccess)
                return RedirectToAction(nameof(Index));
            return RedirectToAction(actionName: "Index", controllerName: "Product");
        }

        private async Task<ResultDto> AddCard(long productId, int count = 1)
        {
            var browserId = _cookies.GetBrowserId(HttpContext);
            var userId = (await CurrentUser())?.Id;
            string ip = "";
            string userAgent = Request.Headers?.FirstOrDefault(s => s.Key.ToLower() == "user-agent").Value;

            if (HttpContext.Connection.RemoteIpAddress != null)
            {
                ip = HttpContext.Connection.RemoteIpAddress.ToString();
            }

            var res = await Mediator.Send(new AddCartCommand
            {
                BrowserId = browserId,
                ProductId = productId,
                Count = count,
                UserId = userId,
                UserIp = ip,
                BrowserName = userAgent
            });
            return res;
        }

        public async Task<IActionResult> AddToBasket(long id)
        {
            var res = await AddCard(id);
            if (res.IsSuccess)
                return RedirectToAction(nameof(Index));
            return RedirectToAction(actionName: "Index", controllerName: "Product");
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCartItem(long id, int count)
        {
            var browserId = _cookies.GetBrowserId(HttpContext);
            var userId = (await CurrentUser())?.Id;
            var res = await Mediator.Send(new UpdateCountCartCommand { BrowserId = browserId, CartItemId = id, Count = count, UserId = userId });
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveCartItem(long id)
        {
            var browserId = _cookies.GetBrowserId(HttpContext);
            var userId = (await CurrentUser())?.Id;
            var res = await Mediator.Send(new RemoveFromCardCommand { BrowserId = browserId, CartItemId = id, UserId = userId });
            return Json(res);
        }

        [Route("shipping")]
        [Authorize]
        public async Task<IActionResult> Shipping()
        {
            var browserId = _cookies.GetBrowserId(HttpContext);
            var userId = (await CurrentUser())?.Id;
            var address = await Mediator.Send(new GetUserAddressQueries
            { ApplicationUserId = userId.GetValueOrDefault() });

            var request = await Mediator.Send(new GetCardShippingQueries
            {
                BrowserId = browserId,
                UserId = userId
            });
            var model = new ShippingViewModel
            {
                CartPayDto = request.Data,
                UserAddressDtos = address
            };
            return View(model);
        }

        [Route("shipping")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Shipping(ShippingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await CurrentUser();
            var userId = user?.Id;
            var res = await Mediator.Send(new AddOrderCommand
            {
                CreatorUserId = userId,
                OrderState = OrderState.Processing,
                OrderPostType = model.OrderPostType,
                UserAddressId = model.UserAddressId
            });
            if (res.IsSuccess)
            {
                if (model.PaymentType == PaymentType.Online)
                {
                    var callbackUrl = "https://www.shoshbazar.ir/Payment/" + res.Data.Id;

#if DEBUG
                    callbackUrl = "https://localhost:5001/Payment/" + res.Data.Id;

#endif
                    var request = new ZarinpalModel.Payment.Request
                    {
                        MerchantId = _merchant,
                        Amount = res.Data.Amount,
                        Description = $"پرداخت شماره فاکتور {res.Data.Id}",
                        CallbackUrl = callbackUrl
                    };
                    var response = ZarinPalRestApi.PaymentRequest(request);
                    if (response.Status == 100)
                        return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{response.Authority}");
                }
                else
                {
                    var payRes = await Mediator.Send(new AddPaymentCommand()
                    {
                        CreatorUserId = userId,
                        RefId = 0,
                        Authority = null,
                        Amount = res.Data.Amount,
                        IsPay = false,
                        PayDate = null,
                        IsRemoved = false,
                        OrderId = res.Data.Id,
                        PaymentType = model.PaymentType

                    });
                    if (payRes.IsSuccess)
                    {
                        var sms = SendSms((await CurrentUser()).PhoneNumber);
                        return View("PaymentResult", new PaymentResultDto { PaymentType = model.PaymentType, IsPay = false, Factor = res.Data.Id.ToString() });
                    }
                }
            }
            return View(model);
        }

        [Route("payment/{factorId}")]
        public async Task<IActionResult> Payment(long factorId, string authority, string status)
        {
            var user = await CurrentUser();
            var userId = user?.Id;
            var model = new PaymentResultDto
            {
                PaymentType = PaymentType.Online,
                IsPay = false,
                Factor = factorId.ToString()
            };
            try
            {
                var amount = await Mediator.Send(new GetOrderByIdQueries { OrderId = factorId });
                var paymentCommand = new AddPaymentCommand
                {
                    CreatorUserId = userId,
                    RefId = 0,
                    Authority = authority,
                    Amount = amount.Amount,
                    IsPay = false,
                    PayDate = null,
                    IsRemoved = false,
                    OrderId = factorId,
                    PaymentType = PaymentType.Online,
                    StatusCode = status,
                    StatusCodeMessage = PaymentResult.ZarinPal(status),

                };
                if (status.ToUpper() == "NOK")
                {
                    model.IsPay = false;
                    model.Message = PaymentResult.ZarinPal(status);

                    await Mediator.Send(new UpdateCartFinished { CartId = factorId });

                }
                else if (status.ToUpper() == "OK")
                {

                    var request = new ZarinpalModel.PaymentVerification.Request
                    {
                        MerchantId = _merchant,
                        Authority = authority,
                        Amount = amount.Amount
                    };
                    var response = ZarinPalRestApi.PaymentVerification(request);
                    if (response.Status == 100)
                    {
                        model.IsPay = true;
                        model.RefId = response.RefId;

                        paymentCommand.IsPay = true;
                        paymentCommand.PayDate = DateTime.Now;
                        paymentCommand.RefId = response.RefId;
                        paymentCommand.StatusCode = response.Status.ToString();
                        paymentCommand.StatusCodeMessage = PaymentResult.ZarinPal(response.Status.ToString());

                        var sms = SendSms((await CurrentUser()).PhoneNumber);

                    }
                    else
                    {
                        model.IsPay = false;
                        model.Message = PaymentResult.ZarinPal(response.Status.ToString());
                        paymentCommand.StatusCode = response.Status.ToString();
                        paymentCommand.StatusCodeMessage = PaymentResult.ZarinPal(response.Status.ToString());
                    }

                }
                var payRes = await Mediator.Send(paymentCommand);
                if (!payRes.IsSuccess)
                {
                    model.IsPay = false;
                    await Mediator.Send(new UpdateCartFinished { CartId = factorId });
                }
            }
            catch (Exception e)
            {
                model.Message = "پاسخی از درگاه پرداخت دریافت نشده";
            }
            return View("PaymentResult", model);
        }

        private bool SendSms(string to)
        {
            var sms = new FarazSmsService("09126035402", "0011062991", "+98500010706035402");
            return sms.SendSms("سلام خوبی", to);
        }
    }
}
