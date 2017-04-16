using Hungsum.Framework.App;
using Hungsum.Framework.Events;
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcPopupPage<T> : PopupPage, IUcPage
    {
        private bool isInitialized = false;

        public event EventHandler<HsEventArgs<HsActionKey, T>> PopupData;

        public UcPopupPage()
        {
            #region 注册回调onInit方法

            this.Appearing += new EventHandler((sender, e) =>
            {
                if (!this.isInitialized)
                {
                    onInit();

                    this.isInitialized = true;
                }
            });

            #endregion
        }

        protected virtual void onInit() { }

        protected void onPopupData(HsActionKey actionKey, T data)
        {
            this.PopupData?.Invoke(this, new HsEventArgs<HsActionKey, T>() { Data = actionKey, Data2 = data });
        }

        #region IUcPage

        public virtual string GetTitle()
        {
            return ((HsApp)Application.Current).Title;
        }

        public virtual string GetBackgroundImage()
        {
            return null;
        }

        public LoginData GetLoginData()
        {
            return ((HsApp)Application.Current).LoginData;
        }

        public SysWSUtil GetWSUtil()
        {
            return ((HsApp)Application.Current).WSUtil;
        }

        public void ShowError(string error)
        {
            HsToastHelper.ShowError(error);
        }

        public void ShowInformation(string text = "处理成功")
        {
            HsToastHelper.ShowSuccess(text);
        }

        public void ShowLoading(string text = "载入中...")
        {
            HsToastHelper.ShowLoading(text);
        }

        public void HideLoading()
        {
            HsToastHelper.HideLoading();
        }

        public virtual void OnPushed() { }

        public virtual void OnPoped() { }

        #region ICommand

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        protected void onCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public virtual void Execute(object parameter)
        {
            var cp = parameter as HsCommandParams;

            if (cp == null) return;

            callAction(cp.ActionKey, cp.Data);
        }

        protected abstract void callAction(HsActionKey actionKey, object item);

        #endregion

        #endregion
    }
}
