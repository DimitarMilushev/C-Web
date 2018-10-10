using System;
using System.Linq;
using System.Net;

namespace Validate_URL
{
    class Program
    {
        static void Main(string[] args)
        {
            string encodedUrl = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(encodedUrl);

            try
            {
                var uri = new Uri(decodedUrl);

                if (uri.Scheme == "https" && uri.Port != 443
                    || uri.Scheme == "http" && uri.Port != 80)
                    throw new ArgumentException("Invalid port");

                Console.WriteLine($"Protocol:{uri.Scheme}" + Environment.NewLine +
                                  $"Host:{uri.Host}" + Environment.NewLine +
                                  $"Port:{uri.Port}" + Environment.NewLine +
                                  $"Path:{uri.AbsolutePath}");

                string query = uri.Query;
                if (!string.IsNullOrEmpty(query))
                    Console.WriteLine($"Query:{query.Substring(1, query.Length - 1)}");

                string fragment = uri.Fragment;
                if (!string.IsNullOrEmpty(fragment))
                    Console.WriteLine($"Fragment:{fragment.Substring(1, fragment.Length - 1)}");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid URL");
            }
        }
    }
}