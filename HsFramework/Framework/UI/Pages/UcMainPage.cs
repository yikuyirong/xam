using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public class UcMainPage : UcCarouselPage, IUcMainPage
    {
        public event EventHandler Logout;

        public UcMainPage(XElement xMenus)
        {

            foreach (XElement xMenu in xMenus.Elements("Menu")) //一级目录
            {
                InnerContentPage page = new InnerContentPage(xMenu);

                page.ItemClick += new EventHandler<HsEventArgs<IHsLabelValue>>((sender, e) =>
                {
                    try
                    {
                        doAction(e.Data);
                    }
                    catch (Exception ex)
                    {
                        this.ShowError(ex.Message);
                    }

                });

                this.Children.Add(page);
            }

        }

        protected override IList<ToolbarItem> onCreateToolbarItems()
        {

            IList<ToolbarItem> items = base.onCreateToolbarItems();

            items.Add(new IconToolbarItem()
            {
                //Icon = "ion-navicon",
                Text = "设置",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.UserDo1),
                Order = ToolbarItemOrder.Primary
            });

            return items;
        }


        protected virtual async void doAction(IHsLabelValue item)
        {
            try
            {
                if (item.Value.StartsWith("Q_") || item.Value.StartsWith("C_"))
                {
                    this.ShowLoading();

                    string result = await this.GetWSUtil().GetQueryNameAndArgs(GetLoginData().ProgressId, item.Value);

                    XElement xData = XElement.Parse(result);

                    await Navigation.PushAsync(new UcQueryConditionPage(xData));
                }
                else
                {
                    throw new HsException($"未找到【{item.Label}】的处理代码");
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
            finally
            {
                this.HideLoading();
            }


        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        private async Task _logout()
        {
            try
            {
                await GetWSUtil().Logout(GetLoginData().ProgressId);
            }
            catch { }
            finally
            {
                this.Logout?.Invoke(this, null);
            }
        }

        /// <summary>
        /// 调用查询
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        private async Task _callQuery(string queryName)
        {
            string result = await this.GetWSUtil().GetQueryNameAndArgs(GetLoginData().ProgressId, queryName);

            XElement xData = XElement.Parse(result);

            await Navigation.PushAsync(new UcQueryConditionPage(xData));
        }

        protected override async void callAction(HsActionKey actionKey, object data)
        {
            try
            {
                if (actionKey == MenuItemKeys.UserDo1)
                {
                    UcHelpPopupPage helpPage = new UcHelpPopupPage();

                    helpPage.PopupData += new EventHandler<HsEventArgs<HsActionKey, object>>(async (sender, e) =>
                    {
                        try
                        {
                            if (e.Data == MenuItemKeys.注销)
                            {
                                await this._logout();
                            }
                            else if (e.Data == MenuItemKeys.修改密码)
                            {
                                await PopupNavigation.PushAsync(new UcChangePasswordPage());
                            }
                        }
                        catch (Exception ex)
                        {
                            this.ShowError(ex.Message);
                        }
                    });

                    await PopupNavigation.PushAsync(helpPage);

                }
                else
                {
                    base.callAction(actionKey, data);
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

        private class InnerContentPage : UcContentPage
        {
            public event EventHandler<HsEventArgs<IHsLabelValue>> ItemClick;

            public InnerContentPage(XElement xMenu)
            {
                //一级菜单名称
                string title = xMenu.Attribute("Mc").Value;

                List<ShortCutItem> items = new List<ShortCutItem>();

                foreach (XElement xSubMenu in xMenu.Elements("Menu"))
                {
                    items.Add(
                        new ShortCutItem()
                        {
                            Label = xSubMenu.Attribute("Mc").Value,
                            Value = xSubMenu.Attribute("Gnbh").Value,
                            Icon = xSubMenu.Attribute("Icon2").Value,
                        });
                }


                this.BackgroundColor = Color.Transparent;

                //Header
                UcHeaderTitle headerTitle = new UcHeaderTitle(title);

                //StackLayout headLayout = new StackLayout()
                //{
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.Start,
                //    Padding = 5,
                //    BackgroundColor = Color.Black,// .White,
                //    Opacity = 0.7
                //};

                //Label header = new Label() { Text = title,TextColor = Color.White,FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)) , HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center };

                //headLayout.Children.Add(header);

                //Main Grid
                Grid grid = new Grid() { ColumnSpacing = 10, RowSpacing = 10 };

                //图标设置3列
                int colCount = 3;

                int rowCount = (int)Math.Ceiling(items.Count / (double)colCount);

                for (int i = 0; i < colCount; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }


                for (int i = 0; i < rowCount; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                }


                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (i * colCount + j < items.Count)
                        {
                            ShortCutItem item = items[i * colCount + j];

                            UcShortCut shortCut = new UcShortCut(item, item.Icon);

                            shortCut.Click += new EventHandler<HsEventArgs<IHsLabelValue>>((sender, e) =>
                            {
                                this.ItemClick?.Invoke(this, e);
                            });

                            grid.Children.Add(shortCut, j, i);
                        }

                    }
                }

                StackLayout layout = new StackLayout();

                layout.Children.Add(headerTitle);
                layout.Children.Add(
                    new ScrollView() { Content = grid });


                Content = layout;
            }

        }

        private class ShortCutItem : IHsLabelValue
        {
            public string Label { get; set; }

            public string Value { get; set; }

            public string Icon { get; set; }
        }
    }
}
