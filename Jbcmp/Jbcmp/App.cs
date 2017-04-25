using Hungsum.Framework.App;
using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using System.Xml.Linq;
using Hungsum.Jbcmp.UI.Page;
using Hungsum.Jbcmp.Utilities;

namespace Hungsum.Jbcmp
{
    public partial class App : HsApp
    {
        public App() : base("JBCMP")
        {
            //本次测试时不要使用127.0.0.1或localhost，会引发connection refused异常。
            //this.WSUtil = new JbcmpWSUtil() { URL = "http://app.jiabaoruye.com.cn/jbcmp/jbcmpwebservice.asmx/" };
            this.WSUtil = new JbcmpWSUtil() { URL = "http://192.168.1.164/jbcmpwebservice.asmx/" };
        }

        protected override Panel_Main getMainPage(XElement xMenus)
        {
            return new Panel_JbcmpMain(xMenus);
        }

        protected override Panel_Welcome_Base getWelcomePage()
        {
            return new Panel_Welcome();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
