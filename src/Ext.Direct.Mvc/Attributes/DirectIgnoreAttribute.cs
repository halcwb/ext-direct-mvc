using System;

namespace Ext.Direct.Mvc.Attributes {

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public sealed class DirectIgnoreAttribute : Attribute {

    }
}
