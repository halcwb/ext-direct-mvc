using System;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    public class DirectController : Controller {

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding) {
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet, null);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
            return Json(data, contentType, contentEncoding, behavior, null);
        }

        protected internal DirectResult Json(object data, params JsonConverter[] converters) {
            return Json(data, null, converters);
        }

        protected internal DirectResult Json(object data, string contentType, params JsonConverter[] converters) {
            return Json(data, contentType, null, converters);
        }
        
        protected internal DirectResult Json(object data, string contentType, Encoding contentEncoding, params JsonConverter[] converters) {
            JsonSerializerSettings settings = (converters != null && converters.Length > 0) ? new JsonSerializerSettings { Converters = converters } : null;
            return Json(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet, settings);
        }

        protected internal virtual DirectResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, JsonSerializerSettings settings) {
            return new DirectResult {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Settings = settings,
                JsonRequestBehavior = behavior
            };
        }
    }
}
