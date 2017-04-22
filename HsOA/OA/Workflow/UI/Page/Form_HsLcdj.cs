using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Models;
using Xamarin.Forms;
using Hungsum.OA.Workflow.UI.Page;
using Hungsum.Framework.Exceptions;
using Hungsum.Jbcmp.OA.Models;
using Hungsum.OA.Utilities;
using Hungsum.Framework.Events;
using Rg.Plugins.Popup.Services;

namespace Hungsum.Jbcmp.OA.Workflow.UI.Page
{
    public abstract class Form_HsLcdj : Form_DJ
    {
        protected bool allowStartRegularLc = false;

        protected bool allowStartFreeLc = false;

        public Form_HsLcdj()
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

            this.ucUserSwitcher = new UcCheckedInput("0,未审批;1,正在审批;2,审批同意;3,审批驳回;4,用户终止", "0,1,2,3,4", true)
            {
                CName = "审批状态",
                AllowEmpty = false,
            };
        }

        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            if (item.GetValueByLabel("Spzt") == "0" && !this.AuditOnly)
            {
                if (this.allowStartFreeLc)
                {
                    items.Add(new MenuItem()
                    {
                        Text = "自由流程",
                        Command = this,
                        CommandParameter = new HsCommandParams(HsOAActionKeys.发起自由流程.SetLabel("开始发起"), item),
                    });
                }

                if (this.allowStartRegularLc)
                {
                    items.Add(new MenuItem()
                    {
                        Text = "规则流程",
                        Command = this,
                        CommandParameter = new HsCommandParams(HsOAActionKeys.发起规则流程.SetLabel("开始发起"), item),
                    });
                }
            }
            else
            {
                items.Add(new MenuItem()
                {
                    Text = "审批记录",
                    Command = this,
                    CommandParameter = new HsCommandParams(HsOAActionKeys.查看流程审批记录, item)
                });

                if (!this.AuditOnly)
                {
                    items.Add(new MenuItem()
                    {
                        Text = "终止流程",
                        Command = this,
                        CommandParameter = new HsCommandParams(HsOAActionKeys.终止流程.SetLabel("终止"), item),
                        IsDestructive = true
                    });
                }
            }

            return items;
        }

        protected override async Task callAction(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == HsOAActionKeys.查看流程审批记录) //查看审批记录
            {
                Form_HsLcspjl_Show form = new Form_HsLcspjl_Show(item.GetValueByLabel("Djlx"), item.GetValueByLabel("DjId"));

                await Navigation.PushAsync(form);

            }
            else
            {
                await base.callAction(actionKey, item);
            }

        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == HsOAActionKeys.发起自由流程) //发起自由流程
            {
                throw new HsException("未实现发起自由流程逻辑");
            }
            else if (actionKey == HsOAActionKeys.发起规则流程) //发起规则流程
            {
                #region 发起规则流程

                LoginData loginData = GetLoginData();

                string fqr = item.GetValueByLabel("Fqr");

                if (fqr != loginData.Username)
                {
                    throw new HsException($"流程只能由【{fqr}】发起。");
                }

                List<HsLabelValue> lcmbs = await ((HsOAWSUtil)GetWSUtil()).GetHsLcmbs(loginData.ProgressId, this.djlx);

                if (lcmbs.Count < 1)
                {
                    throw new HsException("未找到符合条件的流程模版");
                }

                UcAutoCompleteInput.AutoCompleteDataPopupPage page = new UcAutoCompleteInput.AutoCompleteDataPopupPage("流程模版", lcmbs) { UserDo1Text = "确定"};

                page.PopupData += new EventHandler<HsEventArgs<HsActionKey, string>>(async (sender, e) =>
                {
                    try
                    {
                        //通过获取的模版Id发起流程
                        if (!string.IsNullOrWhiteSpace(e.Data2))
                        {
                            string result = await ((HsOAWSUtil)GetWSUtil()).StartRegularHsLc(GetLoginData().ProgressId, e.Data2, item.GetValueByLabel("DjId"));

                            this.ShowInformation(result);

                            await callRetrieve(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowError(ex.Message);
                    }

                });

                await PopupNavigation.PushAsync(page);

                return await Task.FromResult("");

                #endregion
            }
            else if (actionKey == HsOAActionKeys.终止流程) //终止流程
            {
                #region 终止流程

                LoginData loginData = GetLoginData();

                string fqr = item.GetValueByLabel("Fqr");

                if (fqr != loginData.Username)
                {
                    throw new HsException($"流程只能由【{fqr}】终止。");
                }

                string result = await ((HsOAWSUtil)GetWSUtil()).OverHsLc(loginData.ProgressId, this.djlx, item.GetValueByLabel("DjId"));

                this.ShowInformation(result);

                await this.callRetrieve(false);

                return await Task.FromResult("");

                #endregion
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }
    }
}
