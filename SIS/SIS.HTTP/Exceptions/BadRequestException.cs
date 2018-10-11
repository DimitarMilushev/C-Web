using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        const string defaultMessage =
            "The Request was malformed or contains unsupported elements.";

        public const HttpStatusCode statusCode = HttpStatusCode.BadRequest;

        public BadRequestException()
            :base(defaultMessage)
        {
        }

        public BadRequestException(string newMessage)
            :base(newMessage)
        {

        }
    }
}