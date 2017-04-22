using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using System;
using System.Threading.Tasks;

namespace Hungsum.Sdrd.UI.Page
{
    public abstract class Form_Sdrdht_Base : Form_DJ
    {
        public Form_Sdrdht_Base()
        {
            this.uniqueIdField = "HtId";

            this.ucBeginDate = new UcDateInput()
            {
                CName = "开始日期",
                AllowEmpty = false,
                Flag = UcDateInput.MONTHFIRST
            };

            this.ucEndDate = new UcDateInput()
            {
                CName = "结束日期",
                AllowEmpty = false,
                Flag = UcDateInput.NOW
            };

        }

        protected override async Task callAction(HsActionKey actionKey, HsLabelValue item)
        {
            try
            {
                if (actionKey == SysActionKeys.UserDo3) //回款记录
                {
                    await Navigation.PushAsync(new Form_Sdrdhthkjl_Query2(item));
                }
                else
                {
                    await base.callAction(actionKey, item);
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

    }
}
