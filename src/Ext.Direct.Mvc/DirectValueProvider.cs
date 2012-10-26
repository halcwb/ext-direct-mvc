using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ext.Direct.Mvc {

    public class DirectValueProvider : IValueProvider {

        private readonly Dictionary<string, ValueProviderResult> _values = new Dictionary<string, ValueProviderResult>(StringComparer.OrdinalIgnoreCase);

        public DirectValueProvider(DirectRequest directRequest, ParameterDescriptor[] parameterDescriptors) {
            int paramCount = parameterDescriptors.Length;
            Object[] data = directRequest.Data;

            DirectMethod directMethod = DirectProvider.GetCurrent().GetMethod(directRequest.Action, directRequest.Method);
            bool usesNamedArguments = (directMethod != null && directMethod.UsesNamedArguments);

            if (paramCount > 0) {
                if (usesNamedArguments) { // named arguments. match params by name
                    var dataObj = data[0] as JObject;

                    for (int i = 0; i < paramCount; i++) {
                        string pName = parameterDescriptors[i].ParameterName;
                        Type pType = parameterDescriptors[i].ParameterType;
                        JToken value = dataObj != null ? dataObj.SelectToken(pName) : null;
                        object rawValue = null;

                        if (value != null && value.Type != JTokenType.Null && value.Type != JTokenType.Undefined) {
                            Type vType = value.GetType();
                            if (vType == typeof(JObject) && pType != typeof(JObject) ||
                                vType == typeof(JArray) && pType != typeof(JArray)) {

                                rawValue = JsonConvert.DeserializeObject(value.ToString(), pType);
                            } else {
                                rawValue = Convert.ChangeType(value.ToString(), pType);
                            }
                        }

                        string attemptedValue = Convert.ToString(rawValue, CultureInfo.InvariantCulture);
                        _values.Add(pName, new ValueProviderResult(rawValue, attemptedValue, CultureInfo.InvariantCulture));
                    }
                } else {
                    for (int i = 0; i < parameterDescriptors.Length; i++) {
                        object rawValue = data[i];

                        if (rawValue != null) {
                            Type vType = rawValue.GetType();
                            Type pType = parameterDescriptors[i].ParameterType;

                            // Deserialize only objects and arrays and let MVC handle everything else.
                            if (vType == typeof(JObject) && pType != typeof(JObject) ||
                                vType == typeof(JArray) && pType != typeof(JArray)) {

                                rawValue = JsonConvert.DeserializeObject(rawValue.ToString(), pType);
                            }
                        }

                        string attemptedValue = Convert.ToString(rawValue, CultureInfo.InvariantCulture);
                        _values.Add(parameterDescriptors[i].ParameterName,
                                    new ValueProviderResult(rawValue, attemptedValue, CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        public bool ContainsPrefix(string prefix) {
            return true;
        }

        public ValueProviderResult GetValue(string key) {
            if (key == null) {
                throw new ArgumentNullException("key");
            }

            ValueProviderResult vpResult;
            _values.TryGetValue(key, out vpResult);
            return vpResult;
        }
    }
}
