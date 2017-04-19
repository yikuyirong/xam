using Hungsum.Framework.UI.Pages;
using Hungsum.Jbcmp.OA.Workflow.UI.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Jbcmp.UI.Page
{
    public abstract class Form_JbCgspd_Base : Form_HsLcdj
    {
        public Form_JbCgspd_Base()
        {
            this.uniqueIdField = "DjId";
        }

        public override string GetTitle()
        {
            return "采购审批单";
        }
    }
}
