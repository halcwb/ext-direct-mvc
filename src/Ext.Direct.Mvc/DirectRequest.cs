using System;
using System.Web;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    [JsonObject]
    public class DirectRequest {

        public static string DirectRequestKey = "Ext.Direct.Mvc.DirectRequest";

        public const string RequestFormTransactionId = "extTID";
        public const string RequestFormType = "extType";
        public const string RequestFormAction = "extAction";
        public const string RequestFormMethod = "extMethod";
        public const string RequestFileUpload = "extUpload";

        public DirectRequest() { }

        public DirectRequest(HttpRequestBase httpRequest) {
            // This constructor is called in case of Form post only
            IsFormPost = true;
            Action = httpRequest[RequestFormAction] ?? String.Empty;
            Method = httpRequest[RequestFormMethod] ?? String.Empty;
            Type = httpRequest[RequestFormType] ?? String.Empty;
            IsFileUpload = Convert.ToBoolean(httpRequest[RequestFileUpload]);
            TransactionId = Convert.ToInt32(httpRequest[RequestFormTransactionId]);
        }

        public string Action {
            get;
            set;
        }

        public string Method {
            get;
            set;
        }

        [JsonConverter(typeof(RequestDataConverter))]
        public object[] Data {
            get;
            set;
        }

        public string Type {
            get;
            set;
        }

        public bool IsFormPost {
            get;
            set;
        }

        public bool IsFileUpload {
            get;
            set;
        }

        [JsonProperty("tid")]
        public int TransactionId {
            get;
            set;
        }
    }
}
