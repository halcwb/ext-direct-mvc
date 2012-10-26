using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ext.Direct.Mvc.Resources;

namespace Ext.Direct.Mvc {

    public class DirectMethodInvoker : ControllerActionInvoker {

        protected override IDictionary<string, object> GetParameterValues(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
            var directRequest = controllerContext.HttpContext.Items[DirectRequest.DirectRequestKey] as DirectRequest;
            var parametersDict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            ParameterDescriptor[] parameterDescriptors = actionDescriptor.GetParameters();

            if (directRequest == null) {
                throw new NullReferenceException(DirectResources.Common_DirectRequestIsNull);
            }

            if (!directRequest.IsFormPost && directRequest.Data != null) {
                controllerContext.Controller.ValueProvider = new DirectValueProvider(directRequest, parameterDescriptors);
            }

            foreach (ParameterDescriptor parameterDescriptor in parameterDescriptors) {
                parametersDict[parameterDescriptor.ParameterName] = GetParameterValue(controllerContext, parameterDescriptor);
            }
            return parametersDict;
        }
    }
}
