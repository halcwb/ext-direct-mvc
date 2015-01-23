using System;

namespace Ext.Direct.Mvc {

    internal static class TypeExtensions {

        internal static bool HasAttribute<T>(this Type type) where T : Attribute {
            var attribute = type.GetAttribute<T>();
            return attribute != null;
        }

        internal static T GetAttribute<T>(this Type type) where T : Attribute {
            T attribute = null;
            var attributes = (T[])type.GetCustomAttributes(typeof(T), true);
            if (attributes.Length > 0) {
                attribute = attributes[0];
            }
            return attribute;
        }
    }
}
