using System;
using System.Web.Mvc;

namespace Ext.Direct.Mvc {

    public class DirectApiController : Controller {

        private readonly DirectProvider _provider = DirectProvider.GetCurrent();

        [AcceptVerbs("GET")]
        public ActionResult Index() {
            int number;
            bool flag;
            var qs = HttpContext.Request.QueryString;

            _provider.Name = qs["name"];
            _provider.Namespace = qs["ns"] ?? qs["namespace"];
            if (int.TryParse(qs["buffer"], out number))
                _provider.Buffer = number;
            if (int.TryParse(qs["maxRetries"], out number))
                _provider.MaxRetries = number;
            if (int.TryParse(qs["timeout"], out number))
                _provider.Timeout = number;
            _provider.DateFormat = qs["dateFormat"];
            if (bool.TryParse(qs["debug"], out flag))
                _provider.Debug = flag;

            // for integration with the Ext Designer
            var json = (qs["format"] == "json");

            return JavaScript(_provider.ToString(json));
        }
    }
}
