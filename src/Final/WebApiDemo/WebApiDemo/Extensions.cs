using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;

namespace WebApiDemo
{
    public static class Extensions
    {
        public static Uri ApiLink(this UrlHelper urlHelper, int id)
        {
            return new Uri(urlHelper.Link("DefaultApi", new { id = id }));
        }

        public static Uri ApiLink(this UrlHelper urlHelper, int id, string controller, string routeName = "DefaultApi")
        {
            return new Uri(urlHelper.Link(routeName, new { id = id, controller = controller }));
        }

        public static Uri ApiLink(this UrlHelper urlHelper, string controller)
        {
            return new Uri(urlHelper.Link("DefaultApi", new { controller = controller }));
        }
    }
}