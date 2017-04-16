using Hungsum.Framework.App;
using Hungsum.Framework.Utilities;
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public class UcCarouselPage : CarouselPage, IUcPage
    {
        public event EventHandler CanExecuteChanged;

        private bool isInitialized = false;

        public UcCarouselPage()
        {
            NavigationPage.SetBackButtonTitle(this, " ");

            #region 设置背景，背景在此处设置而不在Init中，是为了使背景图显示时不出现闪烁。

            BackgroundImage = GetBackgroundImage();

            #endregion

            #region 注册回调onInit

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

        #region 工具栏

        protected virtual IList<ToolbarItem> onCreateToolbarItems()
        {
            return new List<ToolbarItem>();
        }

        #endregion

        #region init

        protected virtual void onInit()
        {
            #region Title

            string title = GetTitle();

            if (title != null)
            {
                this.Title = GetTitle();
            }

            #endregion

            #region 建立工具栏

            IList<ToolbarItem> toolbarItems = onCreateToolbarItems();

            if (toolbarItems != null && toolbarItems.Count > 0)
            {
                foreach (ToolbarItem item in toolbarItems)
                {
                    ToolbarItems.Add(item);
                }
            }

            #endregion
        }

        #endregion

        #region IUcPage

        public virtual string GetTitle()
        {
            return ((HsApp)Application.Current).Title;
        }

        public string GetBackgroundImage()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return "tilebackground.xml";
                default:
                    return "background.png";
            }
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

        public void ShowLoading(string text = "处理中...")
        {
            HsToastHelper.ShowLoading(text);
        }

        public void HideLoading()
        {
            HsToastHelper.HideLoading();
        }

        public virtual void OnPushed() { }

        public virtual void OnPoped() { }

        protected void onCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            var cp = parameter as HsCommandParams;

            if (cp == null) return;

            try
            {
                this.callAction(cp.ActionKey, cp.Data);
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

        protected virtual void callAction(HsActionKey actionKey, object data)
        {
            switch (actionKey)
            {
                default:
                    throw new HsException($"未知的命令参数【{actionKey}】");
            }
        }

        #endregion
    }
}
