using SIS.HTTP.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Extensions
{
    public static class HttpResponseStatusExtensions
    {
        private static string GetStatusCode(int code)
        {
            switch(code)
            {
                case 200: return "200 OK";
                case 201: return "201 Created";
                case 302: return "302 Found";
                case 303: return "303 See Other";
                case 400: return "400 Bad Request";
                case 401: return "401 Unauthorized";
                case 403: return "403 Forbidden";
                case 404: return "404 Not Found";
                case 500: return "500 Internal Server Error";
            }
            throw new NotSupportedException();
        }

        public static string GetResponseLine(this HttpResponseStatusCode responseStatusCode)
        {
            return GetStatusCode((int)responseStatusCode);
        }
    }
}