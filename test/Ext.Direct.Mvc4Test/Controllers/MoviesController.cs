using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using Ext.Direct.Mvc4Test.Models;

namespace Ext.Direct.Mvc4Test.Controllers {
    public class MoviesController : DirectController {

        private readonly MovieDbContext db = new MovieDbContext();

        public ActionResult GetAll() {
            var movies = db.Movies;
            return Json(movies);
        }
    }
}
