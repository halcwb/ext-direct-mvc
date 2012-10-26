using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;
using Ext.Direct.Mvc.Resources;
using Newtonsoft.Json;

namespace Ext.Direct.Mvc {

    public class DirectProvider {

        private static DirectProvider _current;
        private static readonly object _lockObj = new object();
        private Dictionary<string, DirectAction> _actions;
        private IControllerFactory _factory;

        public string Name { get; set; }

        public string Url {
            get {
                string path = HttpContext.Current.Request.ApplicationPath;
                if (!path.EndsWith("/")) {
                    path += "/";
                }
                return path + "DirectRouter/Index";
            }
        }

        public static DirectProvider GetCurrent() {
            if (_current == null) {
                lock (_lockObj) {
                    if (_current == null) {
                        _current = new DirectProvider();
                        _current.Configure();
                    }
                }
            }
            return _current;
        }

        private void Configure() {
            if (_actions == null) {
                _actions = new Dictionary<string, DirectAction>();
                var assemblies = BuildManager.GetReferencedAssemblies();
                foreach (Assembly assembly in assemblies) {
                    var types = assembly.GetTypes().Where(type =>
                        type.IsPublic &&
                        type.IsSubclassOf(typeof(DirectController)) &&
                        !type.HasAttribute<DirectIgnoreAttribute>()
                    );
                    foreach (Type type in types) {
                        var action = new DirectAction(type);
                        if (_actions.ContainsKey(action.Name)) {
                            throw new Exception(String.Format(DirectResources.DirectProvider_ActionExists, action.Name));
                        }
                        _actions.Add(action.Name, action);
                    }
                }
            }
        }

        public DirectAction GetAction(string actionName) {
            return !_actions.ContainsKey(actionName) ? null : _actions[actionName];
        }

        public DirectMethod GetMethod(string actionName, string methodName) {
            DirectMethod method = null;
            DirectAction action = GetAction(actionName);
            if (action != null) {
                method = action.GetMethod(methodName);
            }
            return method;
        }

        #region ToString

        public override string ToString() {
            return ToString(false);
        }

        public string ToString(bool json) {
            var config = ProviderConfiguration.GetConfiguration();
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            using (var writer = new JsonTextWriter(sw)) {
#if DEBUG
                writer.Formatting = Formatting.Indented;
#else
                writer.Formatting = config.Debug ? Formatting.Indented : Formatting.None;
#endif
                writer.WriteStartObject();
                writer.WriteProperty("type", "remoting");
                writer.WriteProperty("url", Url);
                if (json) {
                    writer.WriteProperty("descriptor", Name ?? config.Name);
                }
                if (!String.IsNullOrEmpty(config.Namespace)) {
                    writer.WriteProperty("namespace", config.Namespace);
                }
                if (config.Buffer.HasValue) {
                    writer.WriteProperty("enableBuffer", config.Buffer.Value);
                }
                if (config.MaxRetries.HasValue) {
                    writer.WriteProperty("maxRetries", config.MaxRetries.Value);
                }
                if (config.Timeout.HasValue) {
                    writer.WriteProperty("timeout", config.Timeout.Value);
                }
                writer.WritePropertyName("actions");
                writer.WriteStartObject();
                foreach (DirectAction action in _actions.Values) {
                    action.WriteJson(writer);
                }
                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            string name = Name ?? config.Name;
            if (name.Contains('.')) {
                name = String.Format("Ext.ns(\"{0}\");\n{1}", name.Substring(0, name.LastIndexOf('.')), name);
            }

            return json ? sb.ToString() : String.Format("{0}={1};", name, sb);
        }

        #endregion

        #region Request execution

        public void Execute(RequestContext requestContext) {
            HttpContextBase httpContext = requestContext.HttpContext;
            _factory = ControllerBuilder.Current.GetControllerFactory();

            if (httpContext.Request.Form.Count == 0) {
                // Raw HTTP Post request(s)

                var reader = new StreamReader(httpContext.Request.InputStream, new UTF8Encoding());
                string json = reader.ReadToEnd();

                if (json.StartsWith("[") && json.EndsWith("]")) {
                    // Batch of requests
                    var requests = JsonConvert.DeserializeObject<List<DirectRequest>>(json);
                    httpContext.Response.Write("[");
                    foreach (DirectRequest request in requests) {
                        ExecuteRequest(requestContext, request);
                        if (request != requests[requests.Count - 1]) {
                            httpContext.Response.Write(",");
                        }
                    }
                    httpContext.Response.Write("]");
                } else {
                    // Single request
                    var request = JsonConvert.DeserializeObject<DirectRequest>(json);
                    ExecuteRequest(requestContext, request);
                }
            } else {
                // Form Post request
                var request = new DirectRequest(httpContext.Request);
                ExecuteRequest(requestContext, request);
            }
        }

        private void ExecuteRequest(RequestContext requestContext, DirectRequest request) {
            if (request == null) {
                throw new ArgumentNullException("request", DirectResources.Common_DirectRequestIsNull);
            }

            HttpContextBase httpContext = requestContext.HttpContext;
            RouteData routeData = requestContext.RouteData;

            routeData.Values["controller"] = request.Action;
            routeData.Values["action"] = request.Method;
            httpContext.Items[DirectRequest.DirectRequestKey] = request;
            var controller = (Controller)_factory.CreateController(requestContext, request.Action);

            DirectAction action = GetAction(request.Action);
            if (action == null) {
                throw new NullReferenceException(String.Format(DirectResources.DirectProvider_ActionNotFound, request.Action));
            }

            DirectMethod method = action.GetMethod(request.Method);
            if (method == null) {
                throw new NullReferenceException(String.Format(DirectResources.DirectProvider_MethodNotFound, request.Method, action.Name));
            }

            if (!method.IsFormHandler && method.Params == null) {
                if (request.Data == null && method.Len > 0 || request.Data != null && request.Data.Length != method.Len) {
                    throw new ArgumentException(String.Format(DirectResources.DirectProvider_WrongNumberOfArguments, request.Method, request.Action));
                }
            }

            try {
                controller.ActionInvoker = new DirectMethodInvoker();
                (controller as IController).Execute(requestContext);
            } catch (DirectException exception) {
                var errorResponse = new DirectErrorResponse(request, exception);
                errorResponse.Write(httpContext.Response);
            } finally {
                _factory.ReleaseController(controller);
            }

            httpContext.Items.Remove(DirectRequest.DirectRequestKey);
        }

        #endregion
    }
}
