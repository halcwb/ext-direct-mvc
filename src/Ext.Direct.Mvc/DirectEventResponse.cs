using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    public class DirectEventResponse : DirectResponse {

        public DirectEventResponse(DirectRequest request) : base(request) { }

        [JsonProperty("type")]
        public string Type {
            get { return "event"; }
        }

        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }

        [JsonProperty("data")]
        public object Data {
            get;
            set;
        }
    }
}
