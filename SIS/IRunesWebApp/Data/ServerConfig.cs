using System;
using System.Collections.Generic;
using System.Text;

namespace IRunesWebApp.Data
{
    internal class ServerConfig
    {
        internal static string ConnectionString => @"Server=.\SQLEXPRESS;Database=IRunes;Integrated Security=True;";
    }
}
