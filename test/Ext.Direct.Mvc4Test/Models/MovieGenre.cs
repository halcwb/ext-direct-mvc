using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ext.Direct.Mvc4Test.Models {
    public class MovieGenre {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int GenreID { get; set; }
    }
}