using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web
{
    class Program
    {
        static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}
