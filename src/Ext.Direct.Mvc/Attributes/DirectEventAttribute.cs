using System;
using Ext.Direct.Mvc.Resources;

namespace Ext.Direct.Mvc {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class DirectEventAttribute : Attribute {

        public DirectEventAttribute(string name) {
            if (String.IsNullOrEmpty(name)) {
                throw new ArgumentException(DirectResources.Common_NullOrEmpty, "name");
            }

            Name = name;
        }

        public string Name {
            get;
            private set;
        }
    }
}
