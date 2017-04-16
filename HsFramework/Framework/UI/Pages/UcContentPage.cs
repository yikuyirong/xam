using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Acr.UserDialogs;
using Hungsum.Framework.App;
using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using System.Windows.Input;
using Hungsum.Framework.Exceptions;

namespace Hungsum.Framework.UI.Pages
{
    public class UcContentPage : ContentPage, IUcPage
    {
        private bool isInitialized = false;

        public UcContentPage()
        {
            NavigationPage.SetBackButtonTitle(this, " ");

            #region BackgroundImage

            string backgroundImage = GetBackgroundImage();

            if (!string.IsNullOrWhiteSpace(backgroundImage))
            {
                this.BackgroundImage = backgroundImage;
            }
            else
            {
                this.BackgroundColor = Color.Transparent;
            }

            #endregion

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

        #region 工具栏

        protected virtual IList<ToolbarItem> OnCreateToolbarItems()
        {
            return new List<ToolbarItem>();
        }

        #endregion

        #region init

        protected virtual void onInit()
        {
            #region 设置默认Title

            Title = GetTitle();

            #endregion


            #region 建立工具栏

            if (OnCreateToolbarItems() != null)
            {
                foreach (ToolbarItem item in OnCreateToolbarItems())
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
            try
            {
                switch (parameter.ToString())
                {
                    default:
                        throw new HsException($"未知的命令参数【{parameter}】");
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

        #endregion

        #endregion
    }
}
