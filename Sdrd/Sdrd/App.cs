using Hungsum.Framework.App;
using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using Hungsum.Sdrd.Utilities;
using System.Xml.Linq;
using Hungsum.Sdrd.UI.Page;

namespace Hungsum.Sdrd
{
    public partial class App : HsApp
    {
        public App() : base("山东热电")
        {
            //本次测试时不要使用127.0.0.1或localhost，会引发connection refused异常。
            //this.WSUtil = new SdrdWSUtil() { URL = "http://192.168.1.104/sdrdwebservice.asmx/" };
            this.WSUtil = new SdrdWSUtil() { URL = "http://app.hungsum.com/sdrd/sdrdwebservice.asmx/" };
            //this.WSUtil = new SdrdWSUtil() { URL = "http://124.128.94.194:8088/sdrdwebservice.asmx/" };


        }

        protected override Panel_Welcome_Base getWelcomePage()
        {
            ImageSource source = ImageSource.FromResource("Hungsum.Sdrd.Assets.Imgs.sdrd_welcome.png");

            return new Panel_Welcome_Base(source);
        }

        protected override Panel_Main getMainPage(XElement xMenus)
        {
            return new Form_SdrdMain(xMenus);
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
