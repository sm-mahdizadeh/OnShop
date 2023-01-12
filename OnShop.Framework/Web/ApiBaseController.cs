using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace OnShop.Framework.Web
{
    public class ApiBaseController <T> : ControllerBase where T : class
    {
        private readonly ICollection<string> _errors = new List<string>();
        protected readonly UserManager<T> UserManager;
        protected readonly IMediator Mediator;

        public ApiBaseController(UserManager<T> userManager, IMediator mediator)
        {
            UserManager = userManager;
            Mediator = mediator;
        }
        protected bool ResponseHasErrors(ValidationResult result)
        {
            if (result == null || result.IsValid) return false;

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            return true;
        }
        protected void AddProcessError(string error)
        {
            _errors.Add(error);
        }

        protected bool IsValidOperation()
        {
            return !_errors.Any();
        }
        protected async Task<T> CurrentUser()
        {
            var user = await UserManager.GetUserAsync(User);
            return user;
        }
    }
}
