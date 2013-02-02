using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc;

namespace Ext.Direct.MvcTest.Controllers {
    public class TreeController : DirectController {

        public ActionResult Load(string nodeId) {
            var array = new ArrayList();
            if (nodeId == "root") {
                for (int i = 0; i <= 5; i++) {
                    array.Add(new {
                        id = "n" + i,
                        text = "Node " + i,
                        leaf = false
                    });
                }
            } else if (nodeId.Length == 2) {
                var num = nodeId.Substring(1);
                for (int i = 0; i <= 5; i++) {
                    array.Add(new {
                        id = nodeId + i,
                        text = "Node " + num + i,
                        leaf = true
                    });
                }
            }
            return Json(array.ToArray());
        }
    }
}
