﻿using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Sdrd.Utilities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdht_Operation : Form_Sdrdht_Base
    {

        public override string GetTitle()
        {
            return "合同维护";
        }

        protected override IList<ToolbarItem> OnCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.OnCreateToolbarItems();

            items.Insert(0, new IconToolbarItem()
            {
                Icon = "ion-plus-round",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.新建) });

            return items;
        }

        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            string htzt = item.GetValueByLabel("Htzt");

            if (htzt == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "删除",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.删除, item),
                    IsDestructive = true
                });
            }
            else
            {
                items.Add(new MenuItem()
                {
                    Text = "回款记录",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.UserDo3, item)
                });
            }

            return items;
        }


        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((SdrdWSUtil)GetWSUtil()).ShowHts(GetLoginData().ProgressId,
                this.ucBeginDate.ControlValue,
                this.ucEndDate.ControlValue,
                "0,1,2",
                this.GetLoginData().Username);
        }

        protected override async Task addItem()
        {
            Panel_Sdrdht panel = new Panel_Sdrdht();

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
            Panel_Sdrdht panel = new Panel_Sdrdht(item);

            if (item.GetValueByLabel("Htzt") == "0")
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
            if (actionKey == SysActionKeys.删除)
            {
                return await this.callRemoteDoData(item, "Delete_Ht");
            } else
            {
                return await base.doDataItem(actionKey, item);
            }
        }

    }
}
