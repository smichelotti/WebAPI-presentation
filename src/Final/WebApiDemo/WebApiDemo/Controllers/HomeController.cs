using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApiDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("drills");
        }

        public ActionResult Docs()
        {
            var apiExlorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
            var apiInfo = apiExlorer.ApiDescriptions.ToLookup(x => x.ActionDescriptor.ControllerDescriptor.ControllerName);
            return View(apiInfo);
        }
    }
}
