using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.OA.Utilities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.OA.Workflow.UI.Page
{
    public abstract class Form_HsLcspjl_Base : Form_DJ
    {
        public event EventHandler<HsEventArgs<HsLabelValue,bool>> OpenDJ;

        public Form_HsLcspjl_Base()
        {
            this.uniqueIdField = "JlId";

        }

        public override string GetTitle()
        {
            return "审批记录";
        }

        protected void onOpenDJ(HsLabelValue item,bool auditOnly)
        {
            this.OpenDJ?.Invoke(this, new HsEventArgs<HsLabelValue, bool>() { Data = item, Data2 = auditOnly });
        }
    }
}
