using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        const string defaultMessage = "The Server has encountered an error.";

        public const HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        public InternalServerErrorException(string message)
            :base(defaultMessage)
        {

        }
    }
}
