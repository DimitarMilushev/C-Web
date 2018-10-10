using System;
using System.Net;

namespace URL_Decode
{
    class Program
    {
        static void Main(string[] args)
        {
            string encodedURL = Console.ReadLine();

            Console.WriteLine(WebUtility.UrlDecode(encodedURL));
        }
    }
}
