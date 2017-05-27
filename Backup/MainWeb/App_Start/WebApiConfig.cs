using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MainWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //wopi/files
            //routes for wopi 
            //- must have path of /wopi/files/....
            config.Routes.MapHttpRoute(
                name: "Contents",
                routeTemplate: "api/wopi/files/{name}/contents",
                defaults: new { controller = "files", action = "GetFile" }
                );

            config.Routes.MapHttpRoute(
                name: "FileInfo",
                routeTemplate: "api/wopi/files/{name}",
                defaults: new { controller = "Files", action = "Get" }
                );

            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
