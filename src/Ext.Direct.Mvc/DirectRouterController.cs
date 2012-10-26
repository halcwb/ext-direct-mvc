using System;
using System.Web.Mvc;

namespace Ext.Direct.Mvc {

    public class DirectRouterController : Controller {

        private readonly DirectProvider _provider = DirectProvider.GetCurrent();

        [AcceptVerbs("POST")]
        [ValidateInput(false)]
        public ActionResult Index() {
            // Process Ext.Direct requests
            _provider.Execute(ControllerContext.RequestContext);
            return new EmptyResult();
        }
    }
}
