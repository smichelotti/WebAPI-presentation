using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo
{
    [LinkBuilder]
	public class DrillsController : ApiController 
	{
		private readonly IDrillRepository drillRepository;

		// If you are using Dependency Injection, you can delete the following constructor
		public DrillsController () : this(new DrillRepository()) 
		{
		}

		public DrillsController (IDrillRepository drillRepository) 
		{
			this.drillRepository = drillRepository;
		}

        public HttpResourceList<Drill> GetAll()
		{
            return new HttpResourceList<Drill>(this.drillRepository.All.ToList());
		}

		public Drill Get(int id)
		{
			var item = this.drillRepository.Find(id);
			if (item == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return item;
		}

		public HttpResponseMessage Post(Drill item)
		{
			if (ModelState.IsValid)
            {
                this.drillRepository.InsertOrUpdate(item);
                this.drillRepository.Save();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = item.Id }));
                return response;
            }
            else
            {
                var validationResults = this.ModelState.SelectMany(m => m.Value.Errors.Select(x => x.ErrorMessage + "(Property: " + m.Key + ")" ));
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.BadRequest, validationResults));
            }
		}

		public HttpResponseMessage Put(int id, Drill item)
		{
			if (ModelState.IsValid && id == item.Id)
            {
                this.drillRepository.InsertOrUpdate(item);
                this.drillRepository.Save();             

                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
		}

		public HttpResponseMessage Delete(int id)
		{
			var item = this.drillRepository.Find(id);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this.drillRepository.Delete(id);
            this.drillRepository.Save();

            return Request.CreateResponse(HttpStatusCode.NoContent, item);
		}
	}
}

