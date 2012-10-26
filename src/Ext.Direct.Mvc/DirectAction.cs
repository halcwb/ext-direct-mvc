using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Ext.Direct.Mvc.Resources;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    public class DirectAction {

        private readonly Dictionary<string, DirectMethod> _methods;

        public string Name {
            get;
            private set;
        }

        public DirectAction(Type type) {
            Name = type.Name;
            if (Name.EndsWith("Controller")) {
                Name = Name.Substring(0, Name.IndexOf("Controller", StringComparison.InvariantCultureIgnoreCase));
            }
            _methods = new Dictionary<string, DirectMethod>();
            Configure(type);
        }

        private void Configure(Type type) {
            var methods = type.GetMethods()
                .Where(x =>
                    x.IsPublic &&
                    (x.ReturnType == typeof(ActionResult) || x.ReturnType.IsSubclassOf(typeof(ActionResult))) &&
                    !x.HasAttribute<DirectIgnoreAttribute>()
                );

            foreach (MethodInfo method in methods) {
                var directMethod = new DirectMethod(method);
                if (_methods.ContainsKey(directMethod.Name)) {
                    throw new Exception(String.Format(DirectResources.DirectAction_MethodExists, directMethod.Name, Name));
                }
                _methods.Add(directMethod.Name, directMethod);
            }
        }

        public DirectMethod GetMethod(string name) {
            return _methods.ContainsKey(name) ? _methods[name] : null;
        }

        public void WriteJson(JsonWriter writer) {
            writer.WritePropertyName(Name);
            writer.WriteStartArray();
            foreach (var method in _methods.Values) {
                method.WriteJson(writer);
            }
            writer.WriteEndArray();
        }
    }
}