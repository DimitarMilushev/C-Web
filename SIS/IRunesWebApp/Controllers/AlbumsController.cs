using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunesWebApp.Controllers
{
    public class AlbumsController : BaseController
    {
        //public IHttpResponse All(IHttpRequest request)
        //{
        //    if (!this.IsAuthenticated(request))
        //    {
        //        return new RedirectResult("/users/login");
        //    }

        //    var albums = this.Context.Albums;

        //    if (albums.Any())
        //    {
        //        var listOfAlbums = string.Empty;
        //        foreach (var album in albums)
        //        {
        //            var albumHtml = $@"<p><a href=""/albums/details/{album.Id}"">{album.Name}</a></p>";
        //            listOfAlbums += albumHtml;
        //        }
        //        this.ViewBag["albumsList"] = listOfAlbums;
        //    }
        //    else
        //    {
        //        this.ViewBag["albumsList"] = "There are currently no albums.";
        //    }

        //    return this.View();
        //}
    }
}
