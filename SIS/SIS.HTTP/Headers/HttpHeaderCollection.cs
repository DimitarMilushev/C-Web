using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            headers = new Dictionary<string, HttpHeader>();
        }
        public void Add(HttpHeader header)
        {
            headers.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            if (headers.ContainsKey(key))
                return true;
            else
            return false;
        }

        public HttpHeader GetHeader(string key)
        {
            if (!headers.ContainsKey(key))
                return null;
            else
                return headers[key];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var header in headers.Values)
            {
                sb.AppendLine(header.Value);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
