using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, HttpCookie> CookieCollection;

        public HttpCookieCollection()
        {
            this.CookieCollection = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            if (!ContainsCookie(cookie.Key))
                this.CookieCollection.Add(cookie.Key, cookie);
        }

        private void Remove(string key)
        {
            if (ContainsCookie(key))
                this.CookieCollection.Remove(key);
        }

        public bool ContainsCookie(string key)
        {
            if (HasCookies() && this.CookieCollection.ContainsKey(key))
                return true;

            return false;
        }

        public HttpCookie GetCookie(string key)
        {
            if (!ContainsCookie(key))
                return null;

            HttpCookie cookie = this.CookieCollection[key];
            Remove(key);

            return cookie;
        }

        public bool HasCookies()
        {
            if(this.CookieCollection.Keys.Count == 0)
                return false;

            return true;
        }

        public override string ToString()
        {
            return string.Join("; ", this.CookieCollection.Values);
        }
    }
}