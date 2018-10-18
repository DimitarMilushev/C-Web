using SIS.WebServer.Attributes.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Attributes
{
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            if (requestMethod.ToUpper() == "DELETE")
                return true;

            return false;
        }
    }
}
