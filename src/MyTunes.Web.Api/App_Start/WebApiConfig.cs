using MyTunes.Web.Api.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;


namespace MyTunes.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Enable cross origin requests
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Services.Replace(typeof(IContentNegotiator), 
                new JsonContentNegotiator(
                    new JsonMediaTypeFormatter() 
                    { 
                            SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() 
                                {
                                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                                    //PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All
                                } 
                    }));


        }
    }
}
