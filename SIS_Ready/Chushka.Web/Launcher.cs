using SIS.Framework;
using System;

namespace Chushka.Web
{
    class Launcher
    {
        static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }
    }
}
