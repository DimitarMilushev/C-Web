using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftUniHttpServer
{
    public class RequestProcessor
    {
        public async Task ProcessClient(TcpClient client)
        {
            var buffer = new byte[10240];
            var stream = client.GetStream();
            Console.WriteLine($"{client.Client.RemoteEndPoint} => {Thread.CurrentThread.ManagedThreadId}");
            var readLength = await stream.ReadAsync(buffer, 0, buffer.Length);
            var requestText = Encoding.UTF8.GetString(buffer, 0, readLength);
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(requestText);
            Console.WriteLine($"{client.Client.RemoteEndPoint} => {Thread.CurrentThread.ManagedThreadId}");

            var responseText = File.ReadAllText("index.html");

            var responseBytes = Encoding.UTF8.GetBytes(
                "HTTP/1.0 200 OK" + Environment.NewLine +
                "Content-Type: text/plain" + Environment.NewLine +
                "Set-Cookie: cookie=Ok Domain=.;" + Environment.NewLine +
                "Content-Length: " + responseText.Length + Environment.NewLine +
                Environment.NewLine + responseText);

            stream.Write(responseBytes);
        }
    }
}
