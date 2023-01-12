using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnShop.Domain.Arrangements.Queries;
using OnShop.Domain.Stores.Commands;
using OnShop.Domain.User.Entities;
using OnShop.Web.Common;
using OnShop.Web.Models;

namespace OnShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        //[Authorize]
        public IActionResult Index(bool? isSuccess = null)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        [HttpGet("About", Name = "About")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("Contact", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet("Term", Name = "Term")]
        public IActionResult Term()
        {
            return View();
        }
        [HttpGet("NotFound", Name = "NotFound")]
        public new IActionResult NotFound()
        {
            return View("NotFound");
        }


    }
}
