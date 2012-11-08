using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext.Direct.Mvc4Test.Models {
    public class Contact {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Employed { get; set; }
    }
}