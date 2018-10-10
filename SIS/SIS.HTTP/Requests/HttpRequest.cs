using System;
using System.Collections.Generic;
using System.Linq;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Sessions;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            FormData = new Dictionary<string, object>();
            QueryData = new Dictionary<string, object>();
            Headers = new HttpHeaderCollection();
            Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent
                = requestString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] requestLine = splitRequestContent[0].Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!IsValidRequestLine(requestLine))
                throw new BadRequestException();

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath();

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());
            this.ParseCookies();

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);

        }

        private void ParseCookies()
        {
            if (!this.Headers.ContainsHeader("Cookie"))
                return;

            int currentIndex = 0;

            string cookiesString = Headers.GetHeader("Cookie").Value;

            if (string.IsNullOrEmpty(cookiesString))
                return;

            foreach(var cookieData in cookiesString.Split("; "))
            {
                string[] cookieParameters = cookiesString.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);

                if (cookieParameters.Length != 2)
                    continue;

                this.Cookies.Add(new HttpCookie(cookieParameters[0], cookieParameters[1], false));
            }
        }

        private void ParseHeaders(string[] requestLine)
        {
            int currentIndex = 0;

            while (!string.IsNullOrEmpty(requestLine[currentIndex]))
            {
                string[] headerData = requestLine[currentIndex++].Split(": ");

                this.Headers.Add(new HttpHeader(headerData[0], headerData[1]));
            }

            if (!this.Headers.ContainsHeader(GlobalConstants.HostHeaderKey))
                throw new BadRequestException();
        }

        private void ParseRequestPath()
        {
            string newPath = string.Empty;

            if (this.Url.Contains('?'))
                newPath = this.Url.Substring(0, this.Url.IndexOf('?'));
            else
                newPath = this.Url;

            this.Path = newPath;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            if (Enum.TryParse(requestLine[0].Capitalize(), out HttpRequestMethod parsedMethod))
                this.RequestMethod = parsedMethod;
            else
                throw new BadRequestException();
        }

        private void ParseQueryParameters()
        {
            if (!this.Url.Contains('?'))
                return;

            var queryString = this.Url.Split(new[] { '?', '#' })[1];

            if (string.IsNullOrWhiteSpace(queryString))
                return;

            var queryParams = queryString.Split('&');

            if (!IsValidReqestQueryString(queryString, queryParams))
                throw new BadRequestException();

            foreach (var queryParameter in queryParams)
            {
                string[] parameterArguments = queryParameter
                    .Split('=', StringSplitOptions.RemoveEmptyEntries);

                this.QueryData.Add(parameterArguments[0], parameterArguments[1]);
            }
        }

        private void ParseFormDataParameters(string formData)
        {
            if (string.IsNullOrEmpty(formData))
                return;

            string[] formDataParams = formData.Split('&');

            foreach (var formDataParameter in formDataParams)
            {
                string[] args = formDataParameter.Split('=', StringSplitOptions.RemoveEmptyEntries);

                this.FormData.Add(args[0], args[1]);
            }


        }

        private void ParseRequestParameters(string formData)
        {
            this.ParseQueryParameters();

            this.ParseFormDataParameters(formData);
        }


        //Validations


        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length == 3 && requestLine[2] == "HTTP/1.1")
                return true;

            return false;
        }

        private bool IsValidReqestQueryString(string queryString, string[] queryParameters)
        {
            if (!string.IsNullOrEmpty(queryString) && queryParameters.Length > 1)
                return true;

            return false;
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpCookieCollection Cookies { get; }

        public IHttpSession Session { get; set; }
    }
}
