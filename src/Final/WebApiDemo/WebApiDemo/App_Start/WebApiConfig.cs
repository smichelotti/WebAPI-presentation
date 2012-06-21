using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Newtonsoft.Json;
using Ninject;
using WebApiDemo.Models;

namespace WebApiDemo
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config, IDependencyResolver dependencyResolver = null)
        {
            // Dependency Resolution
            if (dependencyResolver == null)
            {
                var kernel = new StandardKernel();
                kernel.Bind<ITagRepository>().To<TagRepository>();
                kernel.Bind<IDrillRepository>().To<DrillRepository>();
                config.DependencyResolver = new NinjectDependencyResolver(kernel);
            }
            else
            {
                config.DependencyResolver = dependencyResolver;
            }

            // Routing
            config.Routes.MapHttpRoute(
                name: "ChildApiRoute",
                routeTemplate: "api/tags/{id}/drills",
                defaults: new { controller = "tagdrills" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Media Type Formatters
            config.Formatters.Add(new CsvMediaTypeFormatter());
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings = jsonSettings;
        }
    }
}