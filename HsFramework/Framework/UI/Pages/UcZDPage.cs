using FormsPlugin.Iconize;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcZDPage : UcCarouselPage
    {
        protected List<IControlValue> controls = new List<IControlValue>();

        protected StackLayout mainContent;

        protected StackLayout itemsContent;

        protected ToolbarItem enterToolbarItem;


        public UcZDPage() : base()
        {

        }

        protected override void onInit()
        {
            base.onInit();

            #region 建立主布局

            //条目容器包含于scrollView
            itemsContent = new StackLayout()
            {

                Padding = new Thickness(0, 10, 0, 10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Opacity = 0.6
            };

            //包含于mainContent
            ScrollView scrollView = new ScrollView()
            {
                Padding = new Thickness(20),
                Content = itemsContent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
            };

            //外围布局容器，包含于mainPage
            mainContent = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            mainContent.Children.Add(scrollView);

            //
            UcContentPage mainPage = new UcContentPage();

            mainPage.Content = mainContent;

            Children.Add(mainPage);

            #endregion

            #region 建立主条目，将此方法放在Init中而不是构造函数中是因为有的窗体条目的确定需要籍由构造函数传入的值。

            this.onCreateMainItems();

            #endregion

            #region 添加条目到布局

            foreach (IControlValue control in controls)
            {
                AddItem(new UcFormItem(control));
            }

            #endregion

            #region 建立附加布局

            #endregion
        }

        protected override IList<ToolbarItem> onCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.onCreateToolbarItems();

            this.enterToolbarItem = new ToolbarItem()
            {
                Text = "确定",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.确定),
                Order = ToolbarItemOrder.Primary
            };

            items.Add(this.enterToolbarItem);

            return items;
        }

        #region 单据条目

        protected abstract void onCreateMainItems();

        #endregion

        protected void AddItem(View control)
        {
            this.itemsContent.Children.Add(control);
        }

        protected void validate()
        {
            foreach (IControlValue control in controls)
            {
                control.Validate();
            }
        }

        protected virtual async void callEnter()
        {
            try
            {
                this.validate();

                await this.update();
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected abstract Task<string> update();

        #region ICommand

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var cp = parameter as HsCommandParams;

            if (cp == null) return;

            if (cp.ActionKey == MenuItemKeys.确定)
            {
                this.callEnter();
            }
            else
            {
                base.Execute(parameter);
            }
        }

        #endregion


    }
}
