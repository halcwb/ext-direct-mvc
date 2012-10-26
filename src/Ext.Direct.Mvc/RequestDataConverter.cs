using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ext.Direct.Mvc {

    internal class RequestDataConverter : JsonConverter {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var data = new List<object>();
            JToken dataArray = JToken.ReadFrom(reader);

            if (!dataArray.HasValues) return null;

            if (dataArray is JObject) {
                data.Add(dataArray);
            } else {
                foreach (JToken dataItem in dataArray) {
                    if (dataItem is JValue) {
                        var value = (dataItem as JValue).Value;
                        data.Add(value == null ? null : value.ToString());
                    } else {
                        data.Add(dataItem);
                    }
                }
            }

            return data.ToArray();
        }

        public override bool CanConvert(Type objectType) {
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
