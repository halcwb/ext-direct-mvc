using System.Reflection;
using System.Web.Mvc;
using Ext.Direct.Mvc.Attributes;
using Ext.Direct.Mvc.Extensions;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    public class DirectMethod {

        public string Name {
            get;
            private set;
        }

        public int Len {
            get;
            private set;
        }

        public string[] Params {
            get;
            private set;
        }

        public bool IsFormHandler {
            get;
            private set;
        }

        public bool UsesNamedArguments {
            get;
            private set;
        }

        public string EventName {
            get;
            private set;
        }

        public DirectMethod(MethodBase method) {
            var actionNameAttr = method.GetAttribute<ActionNameAttribute>();
            Name = actionNameAttr != null ? actionNameAttr.Name : method.Name;

            var directEventAttr = method.GetAttribute<DirectEventAttribute>();
            EventName = directEventAttr != null ? directEventAttr.Name : null;

            var useNamedArgsAttr = method.GetAttribute<NamedArgumentsAttribute>();
            if (useNamedArgsAttr == null) {
                Len = method.GetParameters().Length;
            } else {
                var parameterInfos = method.GetParameters();
                Params = new string[parameterInfos.Length];
                for (int i = 0; i < parameterInfos.Length; i++) {
                    Params[i] = parameterInfos[i].Name;
                }
            }

            IsFormHandler = method.HasAttribute<FormHandlerAttribute>();
            UsesNamedArguments = method.HasAttribute<NamedArgumentsAttribute>();
        }

        public void WriteJson(JsonWriter writer) {
            writer.WriteStartObject();
            writer.WriteProperty("name", Name);

            if (Params != null) {
                writer.WriteProperty("params", Params);
            } else {
                writer.WriteProperty("len", Len);
            }

            if (IsFormHandler) {
                writer.WriteProperty("formHandler", true);
            }

            writer.WriteEndObject();
        }
    }
}
