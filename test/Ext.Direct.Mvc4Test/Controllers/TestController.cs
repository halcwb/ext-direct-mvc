using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;

namespace Ext.Direct.Mvc4Test.Controllers {

    public class TestController : DirectController {

        public ActionResult EchoDateTime(DateTime dt) {
            return Json(dt);
        }
    }
}
