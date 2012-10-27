using System.Web;
using System.Web.Mvc;

namespace Ext.Direct.Mvc4Test {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}