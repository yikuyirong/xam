using Hungsum.Framework.App;
using Hungsum.Framework.Events;
using Hungsum.Framework.Extentsions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public class Panel_Login : Panel_ZD, IUcLoginPage
    {
        public event EventHandler<HsEventArgs<XElement>> LoginSuccess;

        private const string LASTLOGINFILENAME = "lastlogin";

        private UcTextInput ucUserName;

        private UcPassword ucPassword;

        private UcCheckedInput ucDb;

        private UcChooseItemBase ucIsSaved;

		private bool _hasRetrieveAcount = false;

		protected bool hasRretrieveAccount
		{
			get
			{
				return _hasRetrieveAcount;
			}
			set
			{
				_hasRetrieveAcount = value;

				this.enterToolbarItem.Text = value ? "登录" : "获取账套";
			}
			
		}


		public Panel_Login()
        {
            //清除现有LoginData
            ((HsApp)Application.Current).LoginData = null;

        }

        protected override async void onInit()
        {
            base.onInit();

			this.hasRretrieveAccount = false;

			try
            {
				await update();
			}
			catch (Exception e)
            {
                this.ShowError(e.Message);
            }


        }

		protected override void onCreateMainItems()
        {
            ucUserName = new UcTextInput() { CName = "姓名", AllowEmpty = false };
            controls.Add(ucUserName);

            ucPassword = new UcPassword() { CName = "密码", AllowEmpty = false };
            controls.Add(ucPassword);

            ucDb = new UcCheckedInput() { CName = "账套", AllowEmpty = false };
            controls.Add(ucDb);

            ucIsSaved = new UcRadioBox { CName = "是否保存登录信息" };
            controls.Add(ucIsSaved);
        }

		protected override void validate()
		{
			if(hasRretrieveAccount)
			{
				base.validate();
			}
		}

        protected override async Task<string> update()
        {
			if (hasRretrieveAccount)
			{
				//获取账套信息
				string data = await ((HsApp)Application.Current).WSUtil.Login(this.ucUserName.ControlValue, this.ucPassword.ControlValue, this.ucDb.ControlValue);

				//登录成功后将用户信息存入Application中，以便全局调用

				XElement xReturnData = XElement.Parse(data);

				((HsApp)Application.Current).LoginData = new LoginData(
					xReturnData.GetFirstElementValue("ProgressId"),
					xReturnData.GetFirstElementValue("UserBh"),
					xReturnData.GetFirstElementValue("UserName"),
					xReturnData.GetFirstElementValue("DeptBh"),
					xReturnData.GetFirstElementValue("DeptMc"),
					xReturnData.GetFirstElementValue("RoleBhs"),
					xReturnData.GetFirstElementValue("RoleMcs"));

				XElement xMenus = xReturnData.Element("Menus");

				//将登录信息存入本地
				try
				{
					XElement xLastLogin = new XElement("Data");

					xLastLogin.Add(new XElement("UserName", this.ucUserName.ControlValue));
					xLastLogin.Add(new XElement("Password", this.ucIsSaved.Checked ? this.ucPassword.ControlValue : ""));
					xLastLogin.Add(new XElement("Account", this.ucDb.ControlValue));
					xLastLogin.Add(new XElement("IsSaved", this.ucIsSaved.Checked));

					IPlatformExtension pe = DependencyService.Get<IPlatformExtension>(DependencyFetchTarget.GlobalInstance);

					//await HsLocalStorageHelper.WriteAllText(LASTLOGINFILENAME, xLastLogin.ToString());
					await pe.WriteTextFile(LASTLOGINFILENAME, xLastLogin.ToString());
				}
				catch (Exception ex)
				{
					this.ShowError(ex.Message);
				}

				this.LoginSuccess?.Invoke(this, new HsEventArgs<XElement> { Data = xMenus });

				return null;
			}else
			{
				string data = await ((HsApp)Application.Current).WSUtil.GetDbs();

				this.ucDb.Datas = data;

				this.hasRretrieveAccount = true;

				try
				{
					IPlatformExtension pe = DependencyService.Get<IPlatformExtension>(DependencyFetchTarget.GlobalInstance);

					//读入上次登录配置信息
					//string lastData = await HsLocalStorageHelper.ReadAllText(LASTLOGINFILENAME, "");
					string lastData = await pe.ReadTextFile(LASTLOGINFILENAME);

					if (!string.IsNullOrWhiteSpace(lastData))
					{
						XElement xLastLogin = XElement.Parse(lastData);
						this.ucUserName.ControlValue = xLastLogin.GetFirstElementValue("UserName");
						this.ucPassword.ControlValue = xLastLogin.GetFirstElementValue("Password");
						this.ucDb.ControlValue = xLastLogin.GetFirstElementValue("Account");
						this.ucIsSaved.Checked = bool.Parse(xLastLogin.GetFirstElementValue("IsSaved", false.ToString()));
					}
				}
				catch (Exception e)
				{
					throw e;
				}

				return null;
			}
        }
    }
}
