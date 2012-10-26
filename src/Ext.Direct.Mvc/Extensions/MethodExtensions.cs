using System;
using System.Reflection;

namespace Ext.Direct.Mvc {

    internal static class MethodExtensions {

        internal static bool HasAttribute<T>(this MethodBase method) where T : Attribute {
            T attribute = method.GetAttribute<T>();
            return attribute != null;
        }

        internal static T GetAttribute<T>(this MethodBase method) where T : Attribute {
            T attribute = null;
            var attributes = (T[])method.GetCustomAttributes(typeof(T), true);
            if (attributes.Length > 0) {
                attribute = attributes[0];
            }
            return attribute;
        }
    }
}
