using FormsPlugin.Iconize;
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;
using Hungsum.Framework.Utilities;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class Form_DJ : Form_Base
    {
        #region djlx

        protected string djlx = "";

        #endregion

        #region AuditOnly

        private bool _auditOnly = false;

        public bool AuditOnly
        {
            get { return _auditOnly; }
            set
            {
                _auditOnly = value;

                onCanExecuteChanged();
            }
        }

        #endregion

        #region UniqueField

        private string _uniqueIdField;

        protected string uniqueIdField
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_uniqueIdField))
                {
                    throw new HsException("未定义uniqueIdField");
                }

                return _uniqueIdField;
            }
            set { _uniqueIdField = value; }
        }

        #endregion

        #region Retrieve Controls

        protected UcDateInput ucBeginDate;

        protected UcDateInput ucEndDate;

        protected UcCheckedInput ucUserSwitcher;

        #endregion

        public Form_DJ() : base() { }

        protected override async void onInit()
        {
            base.onInit();

            try
            {
                await callRetrieve(false);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected async Task callRetrieve(bool isShowCondition)
        {
            if (isShowCondition && (ucBeginDate != null || ucEndDate != null || ucUserSwitcher != null))
            {
                RetrieveConditionPage page = new RetrieveConditionPage(new List<IControlValue>() { ucBeginDate, ucEndDate, ucUserSwitcher });

                page.PopupData += new EventHandler<Events.HsEventArgs<HsActionKey, List<string>>>(async (sender, e) =>
                {
                    try
                    {
                        this.lv.ItemsSource = await retrieve();
                    }
                    catch (Exception ex)
                    {
                        this.ShowError(ex.Message);
                    }
                });

                await PopupNavigation.PushAsync(page);
            }
            else
            {
                this.lv.ItemsSource = await retrieve();
            }
        }

        protected abstract Task<List<HsLabelValue>> retrieve();

        protected override IList<ToolbarItem> OnCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.OnCreateToolbarItems();

            items.Add(new IconToolbarItem()
            {
                Icon = "ion-refresh",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.刷新)
            });

            return items;
        }

        protected override async Task<string> callDoData(HsActionKey actionKey, HsLabelValue item)
        {
            string result = await base.callDoData(actionKey, item);

            if (!string.IsNullOrWhiteSpace(result))
            {
                this.ShowInformation(result);

                await callRetrieve(false);
            }

            return result;

        }

        protected async Task<string> callRemoteDoData(HsLabelValue item, string actionFlag)
        {
            if (string.IsNullOrWhiteSpace(uniqueIdField))
            {
                throw new HsException("未定义uniqueIdField");
            }

            XElement xbhs = new XElement("Data", new XElement("Bh", item.GetValueByLabel(uniqueIdField)));

            return await GetWSUtil().DoDatas(GetLoginData().ProgressId, xbhs.ToString(SaveOptions.DisableFormatting), actionFlag);
        }

        protected override async Task callAction(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.刷新)
            {
                await callRetrieve(true);
            }
            else
            {
                await base.callAction(actionKey, item);
            }
        }

        public class RetrieveConditionPage : UcNormalPopupPage<List<string>>, ICommand
        {

            private List<IControlValue> _controls;

            public RetrieveConditionPage(List<IControlValue> controls)
            {
                Title = "请选择刷新条件";


                _controls = controls;

                foreach (IControlValue control in controls)
                {
                    if (control != null)
                    {
                        mainLayout.Children.Add(new UcFormItem(control));
                    }
                }

                //ControlButton

                this.btnUserDo1.Text = "确定";
                this.btnUserDo2.Text = "取消";

            }

            #region ICommand


            protected override async void callAction(HsActionKey actionKey, object item)
            {
                try
                {
                    if (actionKey == SysActionKeys.UserDo1)
                    {
                        List<string> datas = new List<string>();

                        foreach (IControlValue control in _controls)
                        {
                            control?.Validate();

                            datas.Add(control == null ? string.Empty : control.ControlValue);
                        }

                        this.onPopupData(SysActionKeys.选择数据, datas);

                        await PopupNavigation.PopAsync();
                    }
                    else if (actionKey == SysActionKeys.UserDo2)
                    {
                        await PopupNavigation.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    HsToastHelper.ShowError(ex.Message);
                }
            }



            #endregion
        }
    }
}
