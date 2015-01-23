using System;
using System.Web.Mvc;

namespace Ext.Direct.Mvc.Attributes {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DirectHandleErrorAttribute : FilterAttribute, IExceptionFilter {

        public void OnException(ExceptionContext filterContext) {
            if (filterContext == null) {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.ExceptionHandled) {
                return;
            }

            var exception = filterContext.Exception;
            var directRequest = filterContext.HttpContext.Items[DirectRequest.DirectRequestKey] as DirectRequest;

            if (directRequest != null) {
                var errorResponse = new DirectErrorResponse(directRequest, exception);
                errorResponse.Write(filterContext.HttpContext.Response);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
