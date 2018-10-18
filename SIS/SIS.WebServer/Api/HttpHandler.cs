using System;
using System.IO;
using System.Linq;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;

namespace SIS.WebServer.Api
{
    public class HttpHandler : IHttpHandler
    {
        private ServerRoutingTable serverRoutingTable;

        public HttpHandler(ServerRoutingTable routingTable)
        {
            this.serverRoutingTable = routingTable;
        }

        public IHttpResponse Handle(IHttpRequest httpRequest)
        {
            var isResourceRequest = this.IsResourceRequest(httpRequest);

            if (isResourceRequest)
                return this.HandleRequestResponse(httpRequest.Path);

            if (!this.serverRoutingTable.Routes.ContainsKey(httpRequest.RequestMethod)
                || !this.serverRoutingTable.Routes[httpRequest.RequestMethod].ContainsKey(httpRequest.Path.ToLower()))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            return this.serverRoutingTable.Routes[httpRequest.RequestMethod][httpRequest.Path].Invoke(httpRequest);
        }

        private IHttpResponse HandleRequestResponse(string path)
        {
            var indexOfStartOfExtension = path.LastIndexOf('.');
            var indexOfStartOfNameOfResource = path.LastIndexOf('/');

            var requestPathExtension = path
                .Substring(indexOfStartOfExtension);

            var resourceName = path
                .Substring(
                    indexOfStartOfNameOfResource);

            var resourcePath = GlobalConstants.relativePath
                + "/Resources"
                + $"/{requestPathExtension.Substring(1)}"
                + resourceName;

            if (!File.Exists(resourcePath))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            var fileContent = File.ReadAllBytes(resourcePath);

            return new InlineResouceResult(fileContent, HttpResponseStatusCode.Ok);
        }

        private bool IsResourceRequest(IHttpRequest request)
        {
            var requestPath = request.Path;
            if (requestPath.Contains('.'))
            {
                var requestPathExtension = requestPath
                    .Substring(requestPath.LastIndexOf('.'));
                return GlobalConstants.ResourceExtensions.Contains(requestPathExtension);
            }
            return false;
        }
    }
}
