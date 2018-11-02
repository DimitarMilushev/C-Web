using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Method;
using SIS.Framework.Security;
using System;
using System.Collections.Generic;
using System.Text;
using TORSHIA.Web.Controllers.Base;
using TORSHIA.Web.Services;
using TORSHIA.Web.Services.Contracts;
using TORSHIA.Web.ViewModels;

namespace TORSHIA.Web.Controllers
{
    public class UsersController : BaseController
    {
        private IUsersService usersService;

        public UsersController(UsersServices usersService)
        {
            this.usersService = usersService;
        }   

        public IActionResult Login() => this.View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var userIsValid = this.usersService.UserExistsByUsernameAndPassword
                (model.Username, model.Password);

            if (!userIsValid)
                return RedirectToAction("/users/register");

            var user = this.usersService.GetUserByUsername(model.Username);
            
            this.SignIn(new IdentityUser
            {
                Username = model.Username,
                Roles = new List<string> { user.Role.ToString() }
            });

            return RedirectToAction("/");
        }

        public IActionResult Register() => this.View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            usersService.
                RegisterUser(model.Username, model.Password, model.ConfirmPassword, model.Email);

            this.SignIn(new IdentityUser
            {
                Username = model.Username,
                Email = model.Email
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
