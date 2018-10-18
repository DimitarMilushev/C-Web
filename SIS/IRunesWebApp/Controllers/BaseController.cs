using IRunesWebApp.Data;
using Services;
using SIS.Framework.Controllers.Base;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace IRunesWebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        private const string controllerDef = "Controller";

        private const string directorySeparator = @"\";

        private const string viewsFolder = "Views";

        private const string htmlFileExtension = ".html";

        private const string LayoutViewFileName = "_Layout";
           
        private readonly UserCookieService userCookieService;

        public IDictionary<string, string> ViewBag { get; set; }

        protected IRunesContext Context { get; set; }

        public BaseController()
        {
            Context = new IRunesContext();
            userCookieService = new UserCookieService();
            this.ViewBag = new Dictionary<string, string>();
        }

        private string GetCurrentController() =>
         this.GetType().Name.Replace(controllerDef, string.Empty);

        public bool IsAuthenticated(IHttpRequest request)
        {
            return request.Session.ContainsParameter("username");
        }

        public void SignInUser(string username, IHttpResponse response, IHttpRequest request)
        {
            request.Session.AddParameter("username", username);
            var userCookieValue = this.userCookieService.GetUserCookie(username);
            response.Cookies.Add(new HttpCookie("IRunes_auth", userCookieValue));
        }

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
            var layoutView = GlobalConstants.relativePath +
                viewsFolder +
                directorySeparator +
                LayoutViewFileName;

            var filePath = GlobalConstants.relativePath +
                viewsFolder +
                directorySeparator +
                this.GetCurrentController() +
                directorySeparator +
                viewName +
                htmlFileExtension;

            if (!File.Exists(filePath))
                return new BadRequestResult
                    ($"View {viewName} not found.", HttpResponseStatusCode.NotFound);

           var viewContent = BuildViewContent(filePath.ToString());


            var viewLayout = File.ReadAllText(layoutView);
            var view = viewLayout.Replace("@RenderBody()", viewContent);

            var response = new HtmlResult(view, HttpResponseStatusCode.Ok);

            return response;
        }

        private string BuildViewContent(string filePath)
        {
            var viewContent = File.ReadAllText(filePath);

            foreach (var viewBagKey in ViewBag.Keys)
            {
                var dynamicDataPlaceholder = $"{{{viewBagKey}}}";
                if (viewContent.Contains(dynamicDataPlaceholder))
                {
                    viewContent = viewContent.Replace(
                        dynamicDataPlaceholder,
                        this.ViewBag[viewBagKey]);
                }
            }

            return viewContent;
        }
    }
}
