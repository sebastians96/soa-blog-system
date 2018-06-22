using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace AdminPanel_Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            var r = config.Routes.MapHttpRoute(
                name: "AdminApi",
                routeTemplate: "admin/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
