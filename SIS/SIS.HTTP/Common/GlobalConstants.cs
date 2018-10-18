using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Common
{
    public static class GlobalConstants
    {
        public const string HttpOneProtocolFragment = "HTTP/1.1";

        public const string HostHeaderKey = "Host";

        public const string ContentType = "Content-Type";

        public const string HtmlText = "text/html";

        public const string PlainText = "text/plain";

        public const string HttpNewLine = "\r\n";

        public static string[] ResourceExtensions = { ".js", ".css" };

        public const string relativePath = @"..\..\..\";

        public const string ControllerSufix = "Controller";
    }
}