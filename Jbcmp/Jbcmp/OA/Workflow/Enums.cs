using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Jbcmp.OA.Workflow
{
    public sealed class ELcStyle
    {
        public const string 固定流程 = "0";
        public const string 自由流程 = "1";
    }

    public sealed class ELcbzlx
    {
        public const string 同意审批类 = "0";
        public const string 通知确认类 = "1";
    }

    public sealed class ELcbzspfs
    {
        public const string 一进一退 = "0";

        public const string 半进一退 = "1";

        public const string 全进一退 = "2";

        public const string 一进半退 = "3";

        public const string 半进半退 = "4";

        public const string 一进全退 = "5";
    }
}
