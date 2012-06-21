using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class ContactsController : ApiController
    {
        private static Dictionary<int, Contact> repository = new Dictionary<int, Contact>
        {
            { 1, new Contact { Id = 1, FirstName = "Joe", LastName = "Blow" } },
            { 2, new Contact { Id = 2, FirstName = "John", LastName = "Doe" } }
        };

        public Contact Get(int id)
        {
            Contact contact = null;
            if (repository.TryGetValue(id, out contact))
            {
                return contact;
            }

            throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.NotFound));
        }

        public HttpResponseMessage Post(Contact contact)
        {
            if (this.ModelState.IsValid)
            {
                var newId = repository.Values.Max(x => x.Id) + 1;
                contact.Id = newId;
                repository.Add(newId, contact);

                var response = this.Request.CreateResponse(HttpStatusCode.Created, contact);
                //response.Headers.Location = new Uri(this.Url.Link("DefaultApi", new { id = contact.Id }));
                response.Headers.Location = this.Url.ApiLink(contact.Id);
                return response;
            }
            else
            {
                var validationResult = this.ModelState.SelectMany(item => item.Value.Errors.Select(x => x.ErrorMessage + " (" + item.Key + ")"));
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.BadRequest, validationResult));
            }
        }
    }
}
