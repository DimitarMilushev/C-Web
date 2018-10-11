using IRunesWebApp.Data;
using Services;
using SIS.HTTP.Cookies;
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
    public abstract class BaseController
    {
        private const string relativePath = @"D:\Repos\C# Web\SIS\IRunesWebApp\";

        private const string controllerDef = "Controller";

        private const string directorySeparator = @"\";

        private const string viewsFolder = "Views";

        private const string htmlFileExtension = ".html";
           
        private readonly UserCookieService userCookieService;

        protected IRunesContext Context { get; set; }

        public BaseController()
        {
            Context = new IRunesContext();
            userCookieService = new UserCookieService();

        }

        private string GetCurrentController =>
         this.GetType().Name.Replace(controllerDef, string.Empty);

        public void SignInUser(string username, IHttpRequest request)
        {
            request.Session.AddParameter("username", username);
            var userCookieValue = this.userCookieService.GetUserCookie(username);

            request.Cookies.Add(new HttpCookie("IRunes_auth", userCookieValue));
        }

        protected IHttpResponse View([CallerMemberName] string viewName = "")

        {
            StringBuilder filePath = new StringBuilder();

            string subFolder = String.Empty;

            if (viewName != "Index")
                subFolder = "Users";
            else
                subFolder = "Home";

            filePath.Append(relativePath).Append(viewsFolder).Append(directorySeparator)
                .Append(subFolder).Append(directorySeparator)
                .Append(viewName).Append(htmlFileExtension);

            if (!File.Exists(filePath.ToString()))
                throw new BadRequestException();

            var fileContent =  File.ReadAllText(filePath.ToString());

            var response = new HtmlResult(fileContent, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);

            return response;
        }
    }
}
