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
        public Form_JbCgspd_Query()
        {
            this.AuditOnly = true;
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
