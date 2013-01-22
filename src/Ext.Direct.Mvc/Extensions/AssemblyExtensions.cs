using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ext.Direct.Mvc {

    internal static class AssemblyExtensions {

        internal static IEnumerable<Type> GetLoadableTypes(this Assembly assembly) {
            IEnumerable<Type> types;
            try {
                types = assembly.GetTypes();
            } catch (ReflectionTypeLoadException e) {
                types = e.Types.Where(t => t != null);
            }
            return types;
        }
    }
}
