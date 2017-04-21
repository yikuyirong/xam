using Hungsum.Framework.Utilities;
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;

using System;
using System.Xml.Linq;

using Xamarin.Forms;
using FormsPlugin.Iconize;

namespace Hungsum.Framework.App
{
    public abstract class HsApp : Application
    {
        private SysWSUtil _wsUtil;

        private IconNavigationPage _nvPage;

        public HsApp(string title)
        {
            this.Title = title;

            #region 打开环境界面检查更新

            Panel_Welcome_Base p_welcome = getWelcomePage();

            p_welcome.UpgradeComplete += welcomeUpgradeComplete;

            MainPage = p_welcome;

            #endregion

        }

        private void welcomeUpgradeComplete(object sender, EventArgs e)
        {
            #region 首次登录时的LoginPage

            IUcLoginPage loginpage = getLoginPage();

            loginpage.LoginSuccess += _ucLoginPageLoginSuccessEventHandler;

            #endregion

            _nvPage = new IconNavigationPage(loginpage as Page);

            _nvPage.BackgroundColor = Color.Blue;

            _nvPage.Pushed += new EventHandler<NavigationEventArgs>((sender2, e2) =>
            {
                IUcPage p = e2.Page as IUcPage;

                if (p != null)
                {
                    p.OnPushed();
                }
            });

            _nvPage.Popped += new EventHandler<NavigationEventArgs>((sender2, e2) =>
            {
                IUcPage p = e2.Page as IUcPage;

                if (p != null)
                {
                    p.OnPoped();
                }
            });

            MainPage = _nvPage;
        }

        private async void _ucLoginPageLoginSuccessEventHandler(object sender, Events.HsEventArgs<XElement> e)
        {
            try
            {
                IUcMainPage mainPage = getMainPage(e.Data);

                mainPage.Logout += _ucMainPageLogoutEventHandler;

                await ((Page)_nvPage).Navigation.PushAsync((Page)mainPage);

                if (sender is Page)
                {
                    _nvPage.Navigation.RemovePage((Page)sender);
                }
            }
            catch (Exception ex)
            {
                if (sender is IUcPage)
                {
                    ((IUcPage)sender).ShowError(ex.Message);
                }
            }
        }

        private async void _ucMainPageLogoutEventHandler(object sender, EventArgs e)
        {
            try
            {
                IUcLoginPage loginPage = getLoginPage();

                loginPage.LoginSuccess += _ucLoginPageLoginSuccessEventHandler;

                await ((Page)_nvPage).Navigation.PushAsync((Page)loginPage);

                if (sender is Page)
                {
                    _nvPage.Navigation.RemovePage((Page)sender);
                }
            }
            catch (Exception ex)
            {
                if (sender is IUcPage)
                {
                    ((IUcPage)sender).ShowError(ex.Message);
                }
            }
        }

        public string Title { get; set; }

        public SysWSUtil WSUtil
        {
            get
            {
                if (_wsUtil == null)
                {
                    throw new HsException("【WSUtil】未初始化");
                }
                return _wsUtil;
            }
            set { _wsUtil = value; }
        }

        private LoginData _loginData;

        public LoginData LoginData
        {
            get
            {
                if (_loginData == null)
                {
                    throw new HsException("【LoginData】未初始化，请重新登录");
                }
                return _loginData;
            }

            set { _loginData = value; }
        }

        protected virtual IUcLoginPage getLoginPage()
        {
            return new Panel_Login();

        }

        protected virtual Panel_Main getMainPage(XElement xMenus)
        {
            return new Panel_Main(xMenus);
        }

        protected abstract Panel_Welcome_Base getWelcomePage();

    }
}
