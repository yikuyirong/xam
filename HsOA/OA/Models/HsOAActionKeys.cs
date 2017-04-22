using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Jbcmp.OA.Models
{
    public class HsOAActionKeys : SysActionKeys
    {
        public static HsActionKey 发起自由流程 = new HsActionKey("FREELC", "发起自由流程");

        public static HsActionKey 发起规则流程 = new HsActionKey("REGULARLC", "发起规则流程");

        public static HsActionKey 终止流程 = new HsActionKey("OVERLC", "终止流程");

        public static HsActionKey 查看流程审批记录 = new HsActionKey("VIEWLCSPJL", "查看流程审批记录");

    }
}
