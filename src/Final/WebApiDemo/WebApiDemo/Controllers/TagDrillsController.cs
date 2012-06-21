using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [LinkBuilder]
    public class TagDrillsController : ApiController
    {
        private IDrillRepository drillRepository;

        public TagDrillsController(IDrillRepository drillRepository)
        {
            this.drillRepository = drillRepository;
        }

        public HttpResourceList<Drill> GetByTag(int id)
        {
            var list = this.drillRepository.All.Where(d => d.TagId == id).ToList();
            return new HttpResourceList<Drill>(list);
        }
    }
}
