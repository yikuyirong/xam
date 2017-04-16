using FormsPlugin.Iconize;
using Hungsum.Framework.Models;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdhthkjl_Operation : Form_Sdrdhthkjl_Base
    {

        public override string GetTitle()
        {
            return "回款记录维护";
        }

        protected override IList<ToolbarItem> OnCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.OnCreateToolbarItems();

            items.Insert(0, new IconToolbarItem()
            {
                Icon = "ion-plus-round",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.新建)
            });

            return items;
        }

        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            if (item.GetValueByLabel("Jlzt") == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "提交",
                    Command = this,
                    CommandParameter = new HsCommandParams(MenuItemKeys.UserDo1.SetLabel("提交"), item)
                });

                items.Add(new MenuItem()
                {
                    Text = "删除",
                    Command = this,
                    CommandParameter = new HsCommandParams(MenuItemKeys.删除, item), IsDestructive = true
                });
            }
            else
            {
                items.Add(new MenuItem()
                {
                    Text = "取消提交",
                    Command = this,
                    CommandParameter = new HsCommandParams(MenuItemKeys.UserDo2.SetLabel("取消提交"), item)
                });
            }

            return items;
        }


        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((SdrdWSUtil)GetWSUtil()).ShowHthkjls(GetLoginData().ProgressId,
                this.ucBeginDate.ControlValue,
                this.ucEndDate.ControlValue,
                this.ucUserSwitcher.ControlValue);
        }

        protected override async void addItem()
        {
            try
            {
                Panel_Sdrdhthkjl panel = new Panel_Sdrdhthkjl();

                panel.UpdateComplete += new EventHandler((sender, e) =>
                {
                    this.callRetrieve(false);
                });

                await Navigation.PushAsync(panel);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }



        protected async override void modifyItem(HsLabelValue item)
        {
            try
            {
                Panel_Sdrdhthkjl panel = new Panel_Sdrdhthkjl(item);

                if (item.GetValueByLabel("Jlzt") == "0")
                {
                    panel.UpdateComplete += new EventHandler((sender, e) =>
                    {
                        this.callRetrieve(false);
                    });
                }
                else
                {
                    panel.AuditOnly = true;
                }

                await Navigation.PushAsync(panel);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == MenuItemKeys.删除)
            {
                return await this.doData(item, "Delete_Hthkjl");
            } else if (actionKey == MenuItemKeys.UserDo1)
            {
                return await this.doData(item, "Submit_Hthkjl");
            } else if (actionKey == MenuItemKeys.UserDo2)
            {
                return await this.doData(item, "UnSubmit_Hthkjl");
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }
    }
}
