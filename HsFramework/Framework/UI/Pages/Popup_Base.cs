using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.Utilities;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using System;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcNormalPopupPage<T> : UcPopupPage<T>
    {

        protected StackLayout mainLayout;

        private Label header;

        protected Button btnUserDo1, btnUserDo2;

        public UcNormalPopupPage()
        {
            Animation = new ScaleAnimation(MoveAnimationOptions.Center, MoveAnimationOptions.Center);

            Padding = new Thickness(20, HsDevice.OnPlatform<double>(40, 20), 20, 20);

            StackLayout rootLayout = new StackLayout()
            {
                BackgroundColor = Color.White,

                HorizontalOptions = LayoutOptions.FillAndExpand,

                VerticalOptions = LayoutOptions.Center,
            };

            #region Title

            header = new Label()
            {
                Text = "Title",
                Margin = new Thickness(20),
                FontAttributes = FontAttributes.Bold
            };

            rootLayout.Children.Add(header);

            #endregion

            #region mainLayout

            mainLayout = new StackLayout();

            rootLayout.Children.Add(mainLayout);

            #endregion

            #region ControlButton

            btnUserDo1 = new Button()
            {
                Text = "确定",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.UserDo1,null),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            btnUserDo2 = new Button()
            {
                Text = "取消",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.UserDo2,null),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout controlLayout = new StackLayout()
            {
                Padding = new Thickness(0),
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
            };

			//取消在前，确定在后，符合IOS的设计习惯。
			controlLayout.Children.Add(btnUserDo2);
			controlLayout.Children.Add(btnUserDo1);

            rootLayout.Children.Add(controlLayout);

            #endregion

            Content = rootLayout;

        }

        public new string Title
        {
            set
            {
                base.Title = value;

                if (value != null)
                {
                    this.header.Text = value;
                }
            }
        }

        public string UserDo1Text
        {
            get { return this.btnUserDo1.Text; }
            set { this.btnUserDo1.Text = value; }
        }
        
        public string UserDo2Text
        {
            get { return this.btnUserDo2.Text; }
            set { this.btnUserDo2.Text = value; }
        }

    }
}
