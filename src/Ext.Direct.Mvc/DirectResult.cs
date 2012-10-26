using System;
using System.Web;
using System.Web.Mvc;
using Ext.Direct.Mvc.Resources;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    public class DirectResult : JsonResult {

        public JsonSerializerSettings Settings {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
                throw new InvalidOperationException(DirectResources.JsonRequest_GetNotAllowed);
            }

            HttpResponseBase httpResponse = context.HttpContext.Response;
            var directRequest = context.HttpContext.Items[DirectRequest.DirectRequestKey] as DirectRequest;

            if (directRequest != null) {
                WriteResponse(directRequest, httpResponse);
            } else {
                // Allow regular response when the action is not called through Ext Direct
                WriteContent(httpResponse);
            }
        }

        internal virtual void WriteResponse(DirectRequest directRequest, HttpResponseBase response) {
            var method = DirectProvider.GetCurrent().GetMethod(directRequest.Action, directRequest.Method);
            DirectResponse directResponse;

            if (method.EventName != null) {
                directResponse = new DirectEventResponse(directRequest) {
                    Name = method.EventName,
                    Data = Data,
                    Settings = Settings
                };
            } else {
                directResponse = new DirectDataResponse(directRequest) {
                    Result = Data,
                    Settings = Settings
                };
            }

            directResponse.Write(response, ContentType, ContentEncoding);
        }

        private void WriteContent(HttpResponseBase response) {
            if (!String.IsNullOrEmpty(ContentType)) {
                response.ContentType = ContentType;
            } else {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null) {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null) {
                if (Data is String) {
                    // write strings directly to avoid double quotes around them caused by JsonSerializer
                    response.Write(Data);
                } else {
                    using (JsonWriter writer = new JsonTextWriter(response.Output)) {
                        JsonSerializer serializer = JsonSerializer.Create(Settings);
                        var converter = ProviderConfiguration.GetDefaultDateTimeConverter();
                        if (converter != null) {
                            serializer.Converters.Add(converter);
                        }
#if DEBUG
                        writer.Formatting = Formatting.Indented;
#else
                        writer.Formatting = ProviderConfiguration.GetConfiguration().Debug ? Formatting.Indented : Formatting.None;
#endif
                        serializer.Serialize(writer, Data);
                    }
                }
            }
        }
    }
}
