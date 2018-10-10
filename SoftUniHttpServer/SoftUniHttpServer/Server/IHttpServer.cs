using System;
using System.Collections.Generic;
using System.Text;

namespace SoftUniHttpServer.Server
{
    public interface IHttpServer
    {
        void Start();

        void Stop();
    }
}