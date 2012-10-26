using System;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    public class DirectDataResponse : DirectResponse {

        public DirectDataResponse(DirectRequest request) : base(request) {
            TransactionId = request.TransactionId;
            Action = request.Action;
            Method = request.Method;
        }

        [JsonProperty("type")]
        public virtual string Type {
            get { return "rpc"; }
        }

        [JsonProperty("tid")]
        public int TransactionId {
            get;
            set;
        }

        [JsonProperty("action")]
        public string Action {
            get;
            set;
        }

        [JsonProperty("method")]
        public string Method {
            get;
            set;
        }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public object Result {
            get;
            set;
        }
    }
}
