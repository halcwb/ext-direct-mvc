using System;
using System.Web.Mvc;

namespace Ext.Direct.Mvc {

    public class DirectApiController : Controller {

        private readonly DirectProvider _provider = DirectProvider.GetCurrent();

        [AcceptVerbs("GET")]
        public ActionResult Index() {
            // for integration with the Ext Designer
            bool json = (HttpContext.Request.QueryString["format"] == "json");
            _provider.Name = HttpContext.Request.QueryString["name"];
            return JavaScript(_provider.ToString(json));
        }
    }
}
