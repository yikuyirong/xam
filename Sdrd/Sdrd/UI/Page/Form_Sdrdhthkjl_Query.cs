using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Models;
using Hungsum.Sdrd.Utilities;
using System.Xml.Linq;
using Xamarin.Forms;
using Hungsum.Framework.UI.Cells;
using Hungsum.Framework.UI.Views;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdhthkjl_Query : Form_Sdrdhthkjl_Base
    {

        public override string GetTitle()
        {
            return "回款记录浏览";
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((SdrdWSUtil)GetWSUtil()).ShowHthkjls(GetLoginData().ProgressId,
                this.ucBeginDate.ControlValue,
                this.ucEndDate.ControlValue,
                this.ucUserSwitcher.ControlValue);
        }

        protected async override void modifyItem(HsLabelValue item)
        {
            try
            {
                await Navigation.PushAsync(new Panel_Sdrdhthkjl(item) { AuditOnly = true});
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

    }
}
