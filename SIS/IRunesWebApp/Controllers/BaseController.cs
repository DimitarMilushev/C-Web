using SIS.HTTP.Exceptions;
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

        private string GetCurrentController =>
            this.GetType().Name.Replace(controllerDef, string.Empty);

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
            StringBuilder filePath = new StringBuilder();

            filePath.Append(relativePath).Append(viewsFolder).Append(directorySeparator)
                .Append(viewName).Append(htmlFileExtension);

            if (!File.Exists(filePath.ToString()))
                throw new BadRequestException();

            var fileContent =  File.ReadAllText(filePath.ToString());

            var response = new HtmlResult(fileContent, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);

            return response;
        }
    }
}
