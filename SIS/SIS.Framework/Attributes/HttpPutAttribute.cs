using SIS.WebServer.Attributes.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Attributes
{
    public class HttpPutAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToUpper() == "PUT")
                return true;

            return false;
        }
    }
}
