using FormsPlugin.Iconize;
using Hungsum.Framework.Models;
using Hungsum.Jbcmp.OA.Workflow.UI.Page;
using Hungsum.Jbcmp.UI.Page;
using Hungsum.Jbcmp.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Jbcmp.UI.Page
{
    public class Form_JbCgspd_Operation : Form_JbCgspd_Base
    {

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

            if (item.GetValueByLabel("Spzt") == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "删除",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.删除, item),
                    IsDestructive = true
                });
            }

            return items;
        }


        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((JbcmpWSUtil)GetWSUtil()).ShowJbCgspds(GetLoginData().ProgressId,
                this.ucBeginDate.ControlValue,
                this.ucEndDate.ControlValue,
                this.ucUserSwitcher.ControlValue);
        }

        protected override async Task addItem()
        {
            Panel_JbCgspd panel = new Panel_JbCgspd();

            panel.UpdateComplete += new EventHandler(async (sender, e) =>
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
            Panel_JbCgspd panel = new Panel_JbCgspd(item);

            if (item.GetValueByLabel("Spzt") == "0")
            {
                panel.UpdateComplete += new EventHandler(async (sender, e) =>
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
            if (actionKey == SysActionKeys.删除)
            {
                return await this.callRemoteDoData(item, "Delete_JbCgspd");
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }

    }
}
