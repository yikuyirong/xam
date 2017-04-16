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
    public abstract class UcDJListPage : UcListPage
    {
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



        protected UcDateInput ucBeginDate;

        protected UcDateInput ucEndDate;

        protected UcCheckedInput ucUserSwitcher;

        public UcDJListPage() : base() { }

        protected override void onInit()
        {
            base.onInit();

            callRetrieve(false);
        }

        protected async void callRetrieve(bool isShowCondition)
        {
            try
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
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected override IList<ToolbarItem> OnCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.OnCreateToolbarItems();

            items.Add(new IconToolbarItem()
            {
                Icon = "ion-refresh",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.刷新)
            });

            return items;
        }

        #region 新建

        protected void callNew()
        {
            addItem();
        }

        protected virtual void addItem() { }

        #endregion

        #region 修改

        protected override void itemClick(object item)
        {
            modifyItem(item as HsLabelValue);
        }

        protected virtual void modifyItem(HsLabelValue item) { }

        #endregion

        #region doData

        protected async void callDoData(HsActionKey actionKey, HsLabelValue item)
        {
            try
            {
                if (item == null)
                {
                    throw new HsException("选中数据不是有效的HsLabelValue对象。");
                }

                bool result = await this.DisplayAlert($"是否{actionKey.Label}数据？", "", "是", "否");

                if (result)
                {
                    string data = await this.doDataItem(actionKey, item);

                    this.ShowInformation(data);

                    callRetrieve(false);
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected virtual async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            await Task.Delay(1);

            throw new HsException($"未知的ActionKey【{actionKey}】");
        }

        #endregion


        protected async Task<string> doData(HsLabelValue item, string actionFlag)
        {
            if (string.IsNullOrWhiteSpace(uniqueIdField))
            {
                throw new HsException("未定义uniqueIdField");
            }

            XElement xbhs = new XElement("Data", new XElement("Bh", item.GetValueByLabel(uniqueIdField)));

            return await GetWSUtil().DoDatas(GetLoginData().ProgressId, xbhs.ToString(SaveOptions.DisableFormatting), actionFlag);
        }

        public override void Execute(object parameter)
        {
            try
            {
                var cp = parameter as HsCommandParams;

                if (cp != null)
                {
                    this.callAction(cp.ActionKey, cp.Data as HsLabelValue);
                }
                else
                {
                    base.Execute(parameter);
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

        protected virtual void callAction(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == MenuItemKeys.刷新)
            {
                callRetrieve(true);
            }
            else if (actionKey == MenuItemKeys.新建)
            {
                callNew();
            }
            else
            {
                callDoData(actionKey, item);
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
                    if (actionKey == MenuItemKeys.UserDo1)
                    {
                        List<string> datas = new List<string>();

                        foreach (IControlValue control in _controls)
                        {
                            control?.Validate();

                            datas.Add(control == null ? string.Empty : control.ControlValue);
                        }

                        this.onPopupData(MenuItemKeys.选择数据, datas);

                        await PopupNavigation.PopAsync();
                    }
                    else if (actionKey == MenuItemKeys.UserDo2)
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
