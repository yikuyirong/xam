using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Views
{
    /// <summary>
    /// 用于在导航栏下方，用户区域上方显示少量的信息
    /// </summary>
    public class UcHeaderTitle : StackLayout
    {

        public UcHeaderTitle(string title)
        {
            this.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.VerticalOptions = LayoutOptions.Start;
            this.Padding = 5;
            this.BackgroundColor = Color.Black;
            this.Opacity = 0.7;

            Label header = new Label()
            {
                Text = title,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Center
            };

            this.Children.Add(header);
        }
    }
}
