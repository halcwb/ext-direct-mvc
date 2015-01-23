using Newtonsoft.Json;

namespace Ext.Direct.Mvc.Extensions {

    internal static class JsonExtensions {

        internal static void WriteProperty<T>(this JsonWriter jsonWriter, string name, T value) {
            jsonWriter.WritePropertyName(name);
            jsonWriter.WriteRawValue(JsonConvert.SerializeObject(value));
        }
    }
}
