using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnShop.Domain.Notifications.Commands;
using OnShop.Domain.User.Commands;
using OnShop.Domain.User.Dtos;
using OnShop.Domain.User.Entities;
using System;
using System.Threading.Tasks;
using OnShop.ApplicationServices.Services.Interface;

namespace OnShop.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mediator = mediator;
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View(new RegisterUserDto());
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto register)
        {
            if (!ModelState.IsValid) return View(register);

            var user = new ApplicationUser
            {
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                EmailConfirmed = false,
                RegisteredDate = DateTime.Now
            };
            var res = await _userManager.CreateAsync(user, register.Password);
            if (res.Succeeded)
            {

                if (user.EmailConfirmed)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                    {
                        UserId = user.Id,
                        token = token
                    }, protocol: Request.Scheme);
                    var body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={callbackUrl}> Link </a>";
                    await _emailService.Execute(user.Email, body, "فعال سازی حساب کاربری");
                    return RedirectToAction(nameof(DisplayEmail));
                }

                var command = new AddApplicationUserInfoCommand
                {
                    CreateDate = DateTime.Now,
                    CreatorUserId = user.Id,
                    LastName = register.LastName,
                    FirstName = register.FirstName,
                    ApplicationUserId = user.Id,

                };
                var result = await _mediator.Send(command);

                return RedirectToAction(nameof(Index), "Home");
            }
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }
            return View();
        }

        public IActionResult DisplayEmail()
        {
            return View();
        }

        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            var dto = new LoginUserDto { ReturnUrl = returnUrl };
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto log)
        {
            var userIdentity = User.Identity;
            if (userIdentity != null && userIdentity.IsAuthenticated)
                return RedirectToAction(nameof(Index), "Home");

            if (!ModelState.IsValid) return View(log);
            var user = await _userManager.FindByEmailAsync(log.Email);

            //if (user == null)
            //{
            //   var users = await _userManager.fin
            //}

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var tryCount = user.AccessFailedCount;
                var lockUser = tryCount >= 5;
                var res = await _signInManager.PasswordSignInAsync(user, log.Password, log.RememberMe, lockUser);
                if (res.Succeeded)
                {
                    return Redirect(log.ReturnUrl ?? "/");
                }
                else
                {
                    var command = new NotificationCreateCommand
                    {
                        Title = "تلاش ناموفق برای ورود به سیستم",
                        TargetUserId = user.Id,
                        Type =OnShop.Domain.Enum.NotificationType.Warning
                    };
                    await _mediator.Send(command);
                    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه می باشد.");
                }

                if (res.RequiresTwoFactor)
                {
                    //redirect to page twofactor
                }
                if (res.IsLockedOut)
                {
                    //redirect to page IsLockedOut
                }
            }
            else
            {
                ModelState.AddModelError(nameof(log.Email), "نام کاربری یا رمز عبور اشتباه می باشد.");
            }
            return View(log);
        }

        [Route("logout")]
        //[Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed && await _userManager.IsEmailConfirmedAsync(user))
                {

                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    UserId = user.Id,
                    token = token
                }, protocol: Request.Scheme);
                var body = $"لطفا برای بازیابی رمز عبور حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={callbackUrl}> Link </a>";
                await _emailService.Execute(user.Email, body, "بازیابی رمز عبور حساب کاربری");
                ViewBag.ForgetMessage = "لینک بازیابی رمز عبور ارسال گردید.";
            }
            else
                ViewBag.ForgetMessage = "نام کاربری موجود نمی باشد";

            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
                return BadRequest();
            var user = _userManager.FindByIdAsync(UserId).Result;
            if (user == null)
                return RedirectToAction(nameof(Login));

            var result = await _userManager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index), "Home");
            return RedirectToAction("login");

        }

        public IActionResult ResetPassword(string UserId, string Token)
        {
            return View(new ResetPasswordDto
            {
                TokenId = Token,
                UserId = UserId,
            });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto reset)
        {
            if (!ModelState.IsValid) return View(reset);
            var user = await _userManager.FindByIdAsync(reset.UserId);
            if (user != null)
            {
                var res = await _userManager.ResetPasswordAsync(user, reset.TokenId, reset.Password);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View();
        }
    }
}
