using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;
using Hungsum.OA.Utilities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.OA.Workflow.UI.Page
{
    public class Form_HsDbsx_Operation : Form_HsLcspjl_Base
    {
        public Form_HsDbsx_Operation()
        {
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

            this.ucUserSwitcher = new UcCheckedInput("0,待审批;1,审批同意;2,审批驳回", "0")
            {
                CName = "记录状态",
                AllowEmpty = false,
            };

        }

        public override string GetTitle()
        {
            return "代办事项";
        }


        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            items.Add(new MenuItem()
            {
                Text = "原始单据",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.UserDo1, item)
            });

            items.Add(new MenuItem()
            {
                Text = "查看步骤",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.UserDo2, item)
            });

            if (item.GetValueByLabel("Jlzt") == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "审批",
                    Command = this,
                    CommandParameter = new HsCommandParams(MenuItemKeys.UserDo3, item),
                    IsDestructive = true
                });
            }


            return items;
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((HSOAWSUtl)GetWSUtil()).ShowDbsxs(GetLoginData().ProgressId,
                ucBeginDate.ControlValue,
                ucEndDate.ControlValue,
                ucUserSwitcher.ControlValue);
        }


        //protected async override void modifyItem(HsLabelValue item)
        //{
        //    try
        //    {
        //        Panel_Sdrdkh panel = new Panel_Sdrdkh(item);

        //        panel.UpdateComplete += new EventHandler((sender, e) =>
        //        {
        //            this.callRetrieve(false);
        //        });

        //        await Navigation.PushAsync(panel);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowError(ex.Message);
        //    }
        //}

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == MenuItemKeys.删除)
            {
                return await this.doData(item, "Delete_Kh");
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }

        protected override async void callAction(HsActionKey actionKey, HsLabelValue item)
        {
            try
            {
                if (actionKey == MenuItemKeys.UserDo1) //查看原始单据
                {
                    //await Navigation.PushAsync(new Form_Sdrdlxr_Operation(item));
                }
                else if (actionKey == MenuItemKeys.UserDo2) //查看流程步骤
                {
                    await Navigation.PushAsync(new Form_HsLcspjl_Show(item));
                }
                else if (actionKey == MenuItemKeys.UserDo3) //审批
                {
                    //await Navigation.PushAsync(new Form_Sdrdlxr_Operation(item));
                }
                else
                {
                    base.callAction(actionKey, item);
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }

        }

    }
}
