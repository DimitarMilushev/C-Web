using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Results
{
    public class TextResult : HttpResponse
    {
        public TextResult(string content, HttpResponseStatusCode statusCode)
            :base(statusCode)
        {
            this.Headers.Add(new HttpHeader(GlobalConstants.ContentType, GlobalConstants.PlainText));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
