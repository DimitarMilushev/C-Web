using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            if(User.IsLoggedIn)
            {
                return this.View("Home/IndexLoggedIn");
            }
            return this.View();
        }

    }
}
