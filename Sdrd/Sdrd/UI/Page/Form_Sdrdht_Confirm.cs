using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdht_Confirm : Form_Sdrdht_Base
    {
        public Form_Sdrdht_Confirm()
        {
            this.ucUserSwitcher = new UcCheckedInput("0,未确认;1,确认;2,执行完毕", "0,1", true);
        }

        public override string GetTitle()
        {
            return "合同确认";
        }

        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            string htzt = item.GetValueByLabel("Htzt");

            if (htzt == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "确认",
                    Command = this,
                    CommandParameter = new HsCommandParams(MenuItemKeys.UserDo1.SetLabel("确认"), item) 
                });
            }
            else
            {
                items.Add(new MenuItem()
                {
                    Text = "回款记录",
                    Command = this,
                    CommandParameter = new HsCommandParams(MenuItemKeys.UserDo3, item)
                });

                if (htzt == "1")
                {
                    items.Add(new MenuItem()
                    {
                        Text = "取消确认",
                        Command = this,
                        CommandParameter = new HsCommandParams(MenuItemKeys.UserDo2.SetLabel("取消确认"), item)
                    });
                }

            }
            return items;
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((SdrdWSUtil)GetWSUtil()).ShowHts(GetLoginData().ProgressId,
                this.ucBeginDate.ControlValue,
                this.ucEndDate.ControlValue,
                this.ucUserSwitcher.ControlValue);
        }

        protected async override void modifyItem(HsLabelValue item)
        {
            try
            {
                await Navigation.PushAsync(new Panel_Sdrdht(item) { AuditOnly = true });
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == MenuItemKeys.UserDo1)
            {
                return await this.doData(item, "Confirm_Ht");
            }
            else if (actionKey == MenuItemKeys.UserDo2)
            {
                return await this.doData(item, "UnConfirm_Ht");
            } else
            {
                return await base.doDataItem(actionKey, item);
            }
        }
    }
}
