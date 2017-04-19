using FormsPlugin.Iconize;
using Hungsum.Framework.Models;
using Hungsum.Jbcmp.OA.Models;
using Hungsum.Jbcmp.OA.Workflow.UI.Page;
using Hungsum.Jbcmp.UI.Page;
using Hungsum.Jbcmp.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hungsum.Framework.Extentsions;


using Xamarin.Forms;

namespace Hungsum.Jbcmp.UI.Page
{
    public class Form_JbCgspd_Query : Form_JbCgspd_Base
    {

        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            //删除流程操作选项
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            items.Remove(items.FirstOrDefault(r => r.GetActionKey() == HsOAActionKeys.发起自由流程));
            items.Remove(items.FirstOrDefault(r => r.GetActionKey() == HsOAActionKeys.发起规则流程));
            items.Remove(items.FirstOrDefault(r => r.GetActionKey() == HsOAActionKeys.终止流程));

            return items;
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((JbcmpWSUtil)GetWSUtil()).ShowJbCgspds(GetLoginData().ProgressId, "",
                this.ucBeginDate.ControlValue,
                this.ucEndDate.ControlValue,
                this.ucUserSwitcher.ControlValue);
        }

        protected async override Task modifyItem(HsLabelValue item)
        {
            Panel_JbCgspd panel = new Panel_JbCgspd(item) { AuditOnly = true };

            await Navigation.PushAsync(panel);
        }

    }
}
