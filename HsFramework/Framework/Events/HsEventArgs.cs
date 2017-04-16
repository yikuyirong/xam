using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Events
{
    public class HsEventArgs<T> : EventArgs
    {
        public T Data { get; set; }
    }

    public class HsEventArgs<T1, T2> : EventArgs
    {
        public T1 Data { get; set; }

        public T2 Data2 { get; set; }
    }

    public class HsEventArgs<T1, T2, T3> : EventArgs
    {
        public T1 Data { get; set; }

        public T2 Data2 { get; set; }

        public T3 Data3 { get; set; }
    }

    public class HsEventArgs<T1, T2, T3, T4> : EventArgs
    {
        public T1 Data { get; set; }

        public T2 Data2 { get; set; }

        public T3 Data3 { get; set; }

        public T4 Data4 { get; set; }
    }

}
