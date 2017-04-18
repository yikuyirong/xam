using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hungsum.Framework.Models;
using Hungsum.Jbcmp.OA.Models;
using Hungsum.OA.Workflow.UI.Page;
using Hungsum.Framework.Exceptions;

namespace Hungsum.OA.UI.Page
{
    public class Form_HsOAMain : UcMainPage
    {
        public Form_HsOAMain(XElement xMenus) : base(xMenus) { }

        protected override async Task doAction(IHsLabelValue item)
        {
            switch (item.Value)
            {
                case HsOAFuncKey.HS待办事项:

                    Form_HsDbsx_Operation form = new Form_HsDbsx_Operation();
                    form.OpenDJ += new EventHandler<Framework.Events.HsEventArgs<HsLabelValue>>(async (sender, e) =>
                    {
                        try
                        {
                            string djlx = e.Data.GetValueByLabel("Djlx");
                            string djId = e.Data.GetValueByLabel("DjId");
                            await this.openDJ(djlx, djId);
                        }
                        catch (Exception ex)
                        {
                            this.ShowError(ex.Message);
                        }
                    });
                    await Navigation.PushAsync(form);
                    break;
                default:
                    await base.doAction(item);
                    break;
            }
        }

        protected async Task openDJ(string djlx, string djId)
        {
            switch (djlx)
            {
                default:
                    await Task.Delay(1);
                    throw new HsException($"未知的单据类型【{djlx}】");
            }
        }
    }
}
