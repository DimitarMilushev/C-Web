
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

            ConfigureRouting(serverRoutingTable);

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }

            private static void ConfigureRouting(ServerRoutingTable serverRoutingTable)
        {
            // GET
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/index"] =
                request => new RedirectResult("/");
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] =
                request => new HomeController().Index(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/login"] =
                request => new UsersController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/register"] =
                request => new UsersController().Register(request);
//            serverRoutingTable.Routes[HttpRequestMethod.Get]["/all"] =
//                request => new AlbumsController().All(request);


            // POST
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/login"] =
                request => new UsersController().PostLogin(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/register"] =
                request => new UsersController().PostRegister(request);
        }
    }
}
