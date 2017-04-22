using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdkhjljl_Operation : Form_Sdrdkhjljl_Base
    {

        public override string GetTitle()
        {
            return "客户交流记录维护";
        }

        protected override IList<ToolbarItem> OnCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.OnCreateToolbarItems();

            items.Insert(0, new IconToolbarItem()
            {
                Icon = "ion-plus-round",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.新建)
            });

            return items;
        }

        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            items.Add(new MenuItem()
            {
                Text = "删除",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.删除, item),
                IsDestructive = true
            });

            return items;
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((SdrdWSUtil)GetWSUtil()).ShowKhjljls(GetLoginData().ProgressId,
                this.ucBeginDate.ControlValue,
                this.ucEndDate.ControlValue,
                this.GetLoginData().Username);
        }

        protected override async Task addItem()
        {
            Panel_Sdrdkhjljl panel = new Panel_Sdrdkhjljl();

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

            await Navigation.PushAsync(panel);
        }

        protected async override Task modifyItem(HsLabelValue item)
        {
            Panel_Sdrdkhjljl panel = new Panel_Sdrdkhjljl(item);

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

            await Navigation.PushAsync(panel);
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.删除)
            {
                return await this.callRemoteDoData(item, "Delete_Khjljl");
            } else
            {
                return await base.doDataItem(actionKey, item);
            }
        }
    }
}
