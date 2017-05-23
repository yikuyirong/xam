using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
	public class Form_Sdrdxm_Confirm : Form_DJ
    {
		public override string GetTitle()
        {
            return "项目确认";
        }

		protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            if (item.GetValueByLabel("Xmzt") == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "跟进",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.UserDo1.SetLabel("跟进"), item),
                });
			}
            else
            {
                items.Add(new MenuItem()
                {
                    Text = "取消跟进",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.UserDo2.SetLabel("取消跟进"), item),
                });
            }

            return items;
        }


        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((SdrdWSUtil)GetWSUtil()).ShowXms(GetLoginData().ProgressId,
			                                               this.ucBeginDate.ControlValue,
			                                               this.ucEndDate.ControlValue,
			                                               this.ucUserSwitcher.ControlValue, 
			                                               "0,1",
			                                               "");
        }

		protected async override Task modifyItem(HsLabelValue item)
        {
            Panel_Sdrdxm panel = new Panel_Sdrdxm(item);

			if (item.GetValueByLabel("Xmzt") == "0" && item.GetValueByLabel("Zdr") == GetLoginData().Username)
            {
                panel.UpdateComplete += new EventHandler<HsEventArgs<object>>(async (sender, e) =>
                {
                    try
                    {
                        await this.callRetrieve(false);
                    }
                    catch (Exception ex)
                    {
                        this.ShowError(ex.Message);
                    }
                });
            }
            else
            {
                panel.AuditOnly = true;
            }

            await Navigation.PushAsync(panel);
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
			if (actionKey == SysActionKeys.UserDo1)
            {
                return await this.callRemoteDoData(item, "Submit_Xm");
            } else if (actionKey == SysActionKeys.UserDo2)
            {
                return await this.callRemoteDoData(item, "UnSubmit_Xm");
            } else
            {
                return await base.doDataItem(actionKey, item);
            }
        }
    }
}
