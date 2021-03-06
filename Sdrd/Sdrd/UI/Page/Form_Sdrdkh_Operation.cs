﻿using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdkh_Operation : Form_DJ
    {
        public Form_Sdrdkh_Operation()
        {
            this.uniqueIdField = "KhId";

        }

        public override string GetTitle()
        {
            return "客户维护";
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
                Text = "联系人",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.UserDo1, item)
            });

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
            return await ((SdrdWSUtil)GetWSUtil()).ShowKhs(GetLoginData().ProgressId);
        }

        protected override async Task addItem()
        {
            try
            {
                Panel_Sdrdkh panel = new Panel_Sdrdkh();

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
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }


        protected async override Task modifyItem(HsLabelValue item)
        {
            Panel_Sdrdkh panel = new Panel_Sdrdkh(item);

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

        protected override async Task<string> doDataItem(HsActionKey actionKey,HsLabelValue item)
        {
            if (actionKey == SysActionKeys.删除)
            {
                return await this.callRemoteDoData(item, "Delete_Kh");
            } else
            {
                return await base.doDataItem(actionKey, item);
            }
        }

        protected override async Task callAction(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.UserDo1)
            {
                await Navigation.PushAsync(new Form_Sdrdlxr_Operation(item));
            }
            else
            {
                await base.callAction(actionKey, item);
            }
        }

    }
}
