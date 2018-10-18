using SIS.WebServer;
using System;
using System.Reflection;

namespace SIS.Framework
{
    public class MvcEngine
    {
        public void Run(Server server)
        {
            MvcContext.Get.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;

            try
            {
                server.Run();
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
