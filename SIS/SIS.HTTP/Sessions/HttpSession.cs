using SIS.HTTP.Common;
using SIS.HTTP.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> Sessions;

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNull(id, nameof(id));
            this.Id = id;
            this.Sessions = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));
            this.Sessions.Add(name, parameter);
        }

        public void ClearParameters()
        {
            this.Sessions.Clear();    
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            return this.Sessions.ContainsKey(name);
        }

        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            return this.Sessions.GetValueOrDefault(name, null);
        }
    }
}
