using SIS.HTTP.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> Sessions;

        public HttpSession(string id)
        {
            this.Id = id;
            this.Sessions = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            if (!this.Sessions.ContainsKey(name))
                this.Sessions.Add(name, new object());

            this.Sessions[name] = parameter;
        }

        public void ClearParameters()
        {
            foreach (var parameterKey in this.Sessions.Keys)
                this.Sessions.Remove(parameterKey);      
        }

        public bool ContainsParameter(string name)
        {
            if (this.Sessions.ContainsKey(name))
                return true;

            return false;
        }

        public object GetParameter(string name)
        {
            if (ContainsParameter(name))
                return this.Sessions[name];

            return null;
        }
    }
}
