using System;
using System.Collections.Generic;
using System.Linq;

namespace Request_Parser
{
    class Program
    {
        private const string versionConst = "HTTP/1.1";
        private const string contentConst = "Content-Length:";
        private const string typeConst = "Content-Type: text/plain";
        private const string foundStatus = "OK";
        private const string notFoundStatus = "NotFound";


        static void Main(string[] args)
        {
            Dictionary<string, HashSet<string>> validPaths =
                new Dictionary<string, HashSet<string>>();

            string[] input;

            //Adding Valid Paths!
            while (true)
            {
                input = Console.ReadLine().Split(new[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (input[0] == "END")
                    break;
                string path = '/' + input[0];
                string method = input[1].ToUpper();

                if (!validPaths.ContainsKey(path))
                    validPaths.Add(path, new HashSet<string>());

                validPaths[path].Add(method);
            }

            string[] request = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string requestMethod = request[0];
            string requestUrl = request[1];

            if (validPaths.ContainsKey(requestUrl) && validPaths[requestUrl].Contains(requestMethod))
            {
                Console.WriteLine($"{versionConst} 200 {foundStatus}" + Environment.NewLine +
                                  $"{contentConst} {foundStatus.Length}" + Environment.NewLine +
                                  typeConst + Environment.NewLine + Environment.NewLine +
                                  foundStatus);
            }
            else
            {
                Console.WriteLine($"{versionConst} 404 {notFoundStatus}" + Environment.NewLine +
                                  $"{contentConst} {notFoundStatus.Length}" + Environment.NewLine +
                                  typeConst + Environment.NewLine + Environment.NewLine +
                                  notFoundStatus);
            }


        }
    }
}
