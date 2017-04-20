using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hungsum.Framework.Models;
using Hungsum.OA.UI.Page;
using Hungsum.Jbcmp.Models;
using Hungsum.Jbcmp.Utilities;

namespace Hungsum.Jbcmp.UI.Page
{
    public class Form_JbcmpMain : Form_HsOAMain
    {
        public Form_JbcmpMain(XElement xMenus) : base(xMenus) { }


        protected override async Task doAction(IHsLabelValue item)
        {
            switch (item.Value)
            {
                case JbcmpFuncKey.JB采购审批单:
                    await Navigation.PushAsync(new Form_JbCgspd_Operation());
                    break;
                case JbcmpFuncKey.JB采购审批单浏览:
                    await Navigation.PushAsync(new Form_JbCgspd_Query());
                    break;
                default:
                    await base.doAction(item);
                    break;
            }
        }

        protected override async Task openDJ(string djlx, string djId, bool auditOnly)
        {
            switch (djlx)
            {
                case JbcmpDjlx.JBCGSPD:
                    {
                        HsLabelValue item = await ((JbcmpWSUtil)GetWSUtil()).GetJbCgspd(GetLoginData().ProgressId, djId);

                        Panel_JbCgspd panel = new Panel_JbCgspd(item) { AuditOnly = auditOnly};

                        await Navigation.PushAsync(panel);
                    }
                    break;
                default:
                    await base.openDJ(djlx, djId, auditOnly);
                    break;
            }
        }
    }
}
