using System;
using System.Collections;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    [JsonObject]
    public class DirectErrorResponse : DirectDataResponse {

        public DirectErrorResponse(DirectRequest request, Exception exception) : base(request) {
            ErrorMessage = exception.Message;
            ErrorData = exception.Data.Count > 0 ? exception.Data : null;
#if DEBUG
            Where = GetErrorLocation(exception);
#else
            if (ProviderConfiguration.GetConfiguration().Debug) {
                Where = GetErrorLocation(exception);
            }
#endif
            if (request.IsFormPost) {
                Result = new {success = false};
            }
        }

        [JsonProperty("type")]
        public override string Type {
            get { return "exception"; }
        }

        [JsonProperty("message")]
        public string ErrorMessage {
            get;
            private set;
        }

        [JsonProperty("errorData", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary ErrorData {
            get;
            private set;
        }

        [JsonProperty("where", NullValueHandling = NullValueHandling.Ignore)]
        public string Where {
            get;
            private set;
        }

        private static string GetErrorLocation(Exception exception) {
            return String.Format("{0}\n{1}", exception.GetType(), exception.StackTrace);
        }
    }
}
