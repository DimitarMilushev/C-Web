
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

            // GET
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"]
                = request => new RedirectResult("/");

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"]
                = request => new HomeController().Index(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"]
                = request => new UsersController().Login(request);         

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"]
                = request => new UsersController().Register(request);

            // POST 
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"]
                = request => new UsersController().LoginPost(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/register"]
                = request => new UsersController().RegisterPost(request);

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
