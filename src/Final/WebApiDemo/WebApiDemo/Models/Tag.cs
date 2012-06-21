using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    public class Tag : HttpResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}