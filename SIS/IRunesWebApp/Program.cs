
using IRunesWebApp.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;

namespace IRunesWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Home/Index"]
                = request => new RedirectResult("/");

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"]
                = request => new HomeController().Index(request);
            //(route=”/Home/Index”, route=”/”)

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
