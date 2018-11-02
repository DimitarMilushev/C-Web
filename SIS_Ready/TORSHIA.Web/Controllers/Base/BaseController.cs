using SIS.Framework.ActionResults;
using SIS.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TORSHIA.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected override IViewable View([CallerMemberName] string actionName = "")
        {
            if (this.Identity != null)
            {
                this.Model.Data["Username"] = this.Identity.Username;

                if (this.Identity.Roles.Contains("Admin"))
                {
                    this.Model.Data["LoggedIn"] = "none";
                    this.Model.Data["NotLoggedIn"] = "none";
                    this.Model.Data["UserIsAdmin"] = "block";
                }
                else
                {
                    this.Model.Data["LoggedIn"] = "block";
                    this.Model.Data["NotLoggedIn"] = "none";
                    this.Model.Data["UserIsAdmin"] = "none";

                }
            }
            else
            {
                this.Model.Data["LoggedIn"] = "none";
                this.Model.Data["UserIsAdmin"] = "none";
                this.Model.Data["NotLoggedIn"] = "block";
            }

            return base.View(actionName);
        }
    }
}
