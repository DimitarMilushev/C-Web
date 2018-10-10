using SoftUniHttpServer.Server;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SoftUniHttpServer.Program;

namespace SoftUniHttpServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer();

            server.Start();
        }
    }
}