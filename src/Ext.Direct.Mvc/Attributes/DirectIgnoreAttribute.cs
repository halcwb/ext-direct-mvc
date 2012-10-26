using System;

namespace Ext.Direct.Mvc {

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DirectIgnoreAttribute : Attribute {

    }
}
