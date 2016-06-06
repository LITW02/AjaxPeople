using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using People.Data;

namespace MvcApplication27.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Person> People { get; set; }
    }
}