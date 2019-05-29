using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;

namespace APICenter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();
            //Enabling Cross-Origin 
            //config.EnableCors(new EnableCorsAttribute(origins: "*", headers: "*", methods: "*"));

            //Add Customer Filter
            //config.Filters.Add(new ApiVerifyAttribute());

            // Web API 設定和服務
            //route for namespace (取代原HttpControllerSelector)
            //config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
            //config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}/{id}",
                new { action = RouteParameter.Optional, id = RouteParameter.Optional, namespaceName = new string[] { "APIsCenter.Controllers" } }
            );
        }
    }
}
