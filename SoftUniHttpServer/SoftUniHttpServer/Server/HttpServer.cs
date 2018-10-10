using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace SoftUniHttpServer.Server
{
    public class HttpServer : IHttpServer
    {
        private bool IsWorking;
        private readonly TcpListener tcpListener;
        private readonly RequestProcessor requestProcessor;

        public HttpServer()
        {
            this.tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

            this.requestProcessor = new RequestProcessor();
        }
        public void Start()
        {
            this.tcpListener.Start();
            this.IsWorking = true;
            Console.WriteLine("Server Started....");

            while (this.IsWorking)
            {
                var client = this.tcpListener.AcceptTcpClient();
                requestProcessor.ProcessClient(client);
            }
        }

        public void Stop()
        {
            this.IsWorking = false;
            this.tcpListener.Stop();
        }
    }
}
