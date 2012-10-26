using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;

namespace Ext.Direct.Mvc3Test.Controllers {

    public class BasicController : DirectController {

        public ActionResult Echo(string text, DateTime date) {
            return Json(new {
                text,
                date
            });
        }
    }
}
