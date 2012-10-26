using System;

namespace Ext.Direct.Mvc {

    public class DirectException : ApplicationException {

        public DirectException() { }

        public DirectException(string message) : base(message) { }

        public DirectException(string message, Exception innerException) : base(message, innerException) { }
    }
}
