using Hungsum.Framework.Events;
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

            this.ucUserSwitcher = new UcCheckedInput("0,待审批;1,审批同意;2,审批驳回", "0", true)
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
                CommandParameter = new HsCommandParams(SysActionKeys.UserDo1, item)
            });

            items.Add(new MenuItem()
            {
                Text = "查看步骤",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.UserDo2, item)
            });

            if (item.GetValueByLabel("Jlzt") == "0")
            {
                items.Add(new MenuItem()
                {
                    Text = "审批",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.UserDo3, item),
                    IsDestructive = true
                });
            }


            return items;
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((HsOAWSUtil)GetWSUtil()).ShowDbsxs(GetLoginData().ProgressId,
                ucBeginDate.ControlValue,
                ucEndDate.ControlValue,
                ucUserSwitcher.ControlValue);
        }


        protected override async Task modifyItem(HsLabelValue item)
        {
            await this.callAction(SysActionKeys.UserDo1, item);
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.删除)
            {
                return await this.callRemoteDoData(item, "Delete_Kh");
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }

        protected override async Task callAction(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.UserDo1) //查看原始单据
            {
                this.onOpenDJ(item, true);
            }
            else if (actionKey == SysActionKeys.UserDo2) //查看流程步骤
            {
                await Navigation.PushAsync(new Form_HsLcspjl_Show(item));
            }
            else if (actionKey == SysActionKeys.UserDo3) //审批
            {
                Panel_HsLcspjl panel = new Panel_HsLcspjl(item);
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
            else
            {
                await base.callAction(actionKey, item);
            }
        }

    }
}
