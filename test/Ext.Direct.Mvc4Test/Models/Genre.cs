using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext.Direct.Mvc4Test.Models {
    public class Genre {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}