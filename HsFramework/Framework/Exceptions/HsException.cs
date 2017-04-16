using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Exceptions
{
    public class HsException : Exception
    {
        public HsException() : base() { }

        public HsException(string message) : base(message) { }

        public HsException(string message, Exception innerException) : base(message, innerException) { }

        public HsException(int code, string message):this(message)
        {
            this.ExceptionCode = code;
        }

        public int ExceptionCode { get; set; }
    }
}
