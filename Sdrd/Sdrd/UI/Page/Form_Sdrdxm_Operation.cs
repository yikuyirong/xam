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
    public class Form_Sdrdxm_Operation : Form_DJ
    {
        public Form_Sdrdxm_Operation()
        {
            this.uniqueIdField = "XmId";

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

            this.ucUserSwitcher = new UcCheckedInput("0,联系中;10,进行中;15,暂停;20,合作;30,未合作;35,用户终止", "0,10,15,20,30,35")
            {
                CName = "项目进度",
                AllowEmpty = false,
            };

        }

        public override string GetTitle()
        {
            return "项目维护";
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

            if (item.GetValueByLabel("Xmzt") == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "跟进",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.UserDo1.SetLabel("跟进"), item),
                });

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
                this.ucUserSwitcher.ControlValue, "0,1");
        }

        protected override async Task addItem()
        {
            Panel_Sdrdxm panel = new Panel_Sdrdxm();

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
            Panel_Sdrdxm panel = new Panel_Sdrdxm(item);

            if (item.GetValueByLabel("Xmzt") == "0")
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
                return await this.callRemoteDoData(item, "Delete_Xm");
            } else if (actionKey == SysActionKeys.UserDo1)
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
