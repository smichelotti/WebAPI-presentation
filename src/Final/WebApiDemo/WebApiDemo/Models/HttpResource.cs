using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public abstract class HttpResource
    {
        public HttpResource()
        {
            this.Links = new List<Link>();
        }

        [NotMapped]
        public string SelfLink { get; set; }

        [NotMapped]
        public List<Link> Links { get; set; }
    }


    public class HttpResourceList<T> : HttpResource
    {
        private List<T> resourceList;

        public HttpResourceList()
        {

        }
        public HttpResourceList(List<T> resourceList)
        {
            this.resourceList = (resourceList ?? new List<T>());
        }

        public List<T> Items
        {
            get
            {
                return this.resourceList;
            }
            set
            {
                this.resourceList = value;
            }
        }
    }

    public class Link
    {
        public string LinkName { get; set; }
        public string Href { get; set; }
    }
}