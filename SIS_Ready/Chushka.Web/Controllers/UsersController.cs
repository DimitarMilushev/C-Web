using Chushka.Models;
using Chushka.Web.Controllers.Base;
using Chushka.Web.Services;
using Chushka.Web.Services.Contracts;
using Chushka.Web.ViewModels;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Method;
using SIS.Framework.Security;
using SIS.HTTP.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login() => this.View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!this.usersService.UserExistsByUsernameAndPassword(model.Username, model.Password))
                return RedirectToAction("/Users/Register");

            User user = this.usersService.GetUserByUsername(model.Username);

            this.SignIn(new IdentityUser
            {
                Username = user.Username,
                Roles = new List<string> { user.Role.ToString() }
            });

            return RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Register() => this.View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
                throw new BadRequestException("Passwords are not the same.");

            if (this.usersService.UserExistsByUsernameAndPassword(model.Username, model.Password))
                return RedirectToAction("/Users/Register");

            this.usersService.RegisterUser
                (model.Username, model.Password, model.FullName, model.Email);

            this.SignIn(new IdentityUser
            {
                Username = model.Username,
                Email = model.Email,
            });

            return RedirectToAction("/");
        }

        public IActionResult Logout()
        {
            this.SignOut();
            return RedirectToAction("/");
        }
    }
}