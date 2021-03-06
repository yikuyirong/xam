﻿using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using Hungsum.Framework.Extentsions;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Xml.Linq;

namespace Hungsum.Framework.UI.Pages
{
    public class Popup_Help : UcPopupPage<object>
    {
        protected StackLayout mainLayout;

        public Popup_Help()
        {
            Animation = new MoveAnimation(MoveAnimationOptions.Right, MoveAnimationOptions.Right);

			//Padding = new Thickness(200, 0, 0, 0);

			mainLayout = new StackLayout()
			{
				BackgroundColor = Color.White,

				HorizontalOptions = LayoutOptions.End, //.FillAndExpand,

                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            #region HeaderControl

            IconButton headerControl = new IconButton()
            {
                Text = "ion-close-round",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.关闭),
                VerticalOptions = LayoutOptions.End,
            };

            mainLayout.Children.Add(headerControl);

            #endregion

            #region Header

            mainLayout.Children.Add(new LabelItem($"欢迎，{GetLoginData().Username}", "ion-person", FontAttributes.Bold));

            string version = HsDependencyService<IPlatformExtension>.Instance().GetApplicationVersion();

            mainLayout.Children.Add(new LabelItem($"V{version}", "", FontAttributes.Italic));

            #endregion

            #region MainInformation

            ScrollView sc = new ScrollView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout infoLayout = new StackLayout();

            if (!string.IsNullOrWhiteSpace(GetLoginData().Deptmc))
            {
                infoLayout.Children.Add(new LabelItem("所属单位", "ion-home", FontAttributes.Bold));

                infoLayout.Children.Add(new LabelItem(GetLoginData().Deptmc));
            }

            infoLayout.Children.Add(new LabelItem("所属角色", "ion-person-stalker", FontAttributes.Bold));

            foreach (string role in GetLoginData().Rolemcs.Split(';'))
            {
                infoLayout.Children.Add(new LabelItem(role.Trim(new char[] { ';' })));
            }

            sc.Content = infoLayout;

            mainLayout.Children.Add(sc);

            #endregion

            #region FootControl

            mainLayout.Children.Add(new Button()
            {
                Text = "清除缓存",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.UserDo1)
            });

			mainLayout.Children.Add(new Button()
			{
				Text = "检查更新",
				Command = this,
				CommandParameter = new HsCommandParams(SysActionKeys.UserDo2)
			});


			mainLayout.Children.Add(new Button()
            {
                Text = "修改密码",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.修改密码)
            });

            mainLayout.Children.Add(new Button()
            {
                Text = "注销",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.注销)
            });


            #endregion

            Content = mainLayout;
        }

		protected virtual async Task<HsLabelValue> getLastestIPAInfo()
		{
			string result = await GetWSUtil().GetIOSClientInfo();

			return XElement.Parse(result).ToHsLabelValue();
		}


		protected override async void callAction(HsActionKey actionKey, object item)
        {
            try
            {
                if (actionKey == SysActionKeys.关闭)
                {
                    await PopupNavigation.PopAsync();
                }
                else if (actionKey == SysActionKeys.修改密码)
                {
                    callAction(SysActionKeys.关闭, null);

                    this.onPopupData(actionKey, null);

                }
                else if (actionKey == SysActionKeys.UserDo1)
                {
                    //清除缓存
                    long size = await HsDependencyService<IPlatformExtension>.Instance().DirectoryDelete("Cache");

                    await this.DisplayAlert("缓存删除成功。", size != 0 ? $"释放空间{size.GetFileSizeString()}" : "无缓存文件", "确定");

                    callAction(SysActionKeys.关闭, null);
				}else if(actionKey == SysActionKeys.UserDo2)
				{
					//获取新版本
					HsLabelValue lvVersion = await getLastestIPAInfo();

					//取版本号
					HsVersion version = HsVersion.Parse(lvVersion.GetValueByLabel("Version"));

					string upgradeUri = lvVersion.GetValueByLabel("UpgradeURI");

					//检查版本最后一位，如果是奇数表明是一般更新，偶数表明是强制更新
					IPlatformExtension pe = DependencyService.Get<IPlatformExtension>();

					HsVersion currentVersion = HsVersion.Parse(pe.GetApplicationVersion());

					if (pe != null && version > currentVersion) //存在新版本
					{
						if (version.Type == HsVersion.EType.Force)
						{
							await this.DisplayAlert($"发现新版本 {version}，请立即更新", $"当前版本 {currentVersion}", "确定");

							pe.OpenURL(upgradeUri);
						}
						else
						{
							if (await this.DisplayAlert($"发现新版本 {version}，是否更新", $"当前版本 {currentVersion}", "是", "否"))
							{
								pe.OpenURL(upgradeUri);
							}
						}
					}
					else
					{
						await this.DisplayAlert("当前是最新版本",$"当前版本 {currentVersion}", "确定");
					}

					callAction(SysActionKeys.关闭, null);
				}
                else if (actionKey == SysActionKeys.注销)
                {
                    //注销
                    if (await this.DisplayAlert("是否要注销？", "", "是", "否"))
                    {
                        this.onPopupData(actionKey, null);

                        callAction(SysActionKeys.关闭, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private class LabelItem : StackLayout
        {
            public LabelItem(string text, string icon = "", FontAttributes fontAttr = FontAttributes.None)
            {
                Margin = new Thickness(20);

                Orientation = StackOrientation.Horizontal;

                if (!string.IsNullOrWhiteSpace(icon))
                {
                    this.Children.Add(new IconLabel()
                    {
                        Text = icon,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                    });
                }

                this.Children.Add(new Label()
                {
                    Text = text,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = fontAttr,
                });
            }

        }
    }
}
