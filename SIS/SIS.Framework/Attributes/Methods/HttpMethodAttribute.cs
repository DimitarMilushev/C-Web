using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Attributes.Methods
{
    public abstract class HttpMethodAttribute : Attribute
    {
        public abstract bool IsValid(string requestMethod);
    }
}
