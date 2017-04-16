using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Views
{
    public class UcFormItem : StackLayout
    {
        public UcFormItem(IControlValue control)
        {
            Padding = new Thickness(10, 5, 10, 5);
            Orientation = StackOrientation.Horizontal;

            Label title = new Label() { Text = control.CName, MinimumWidthRequest = 100, VerticalOptions = LayoutOptions.CenterAndExpand };

            Label star = new Label() { Text = "*", TextColor = control.AllowEmpty ? Color.Transparent : Color.Red, VerticalOptions = LayoutOptions.CenterAndExpand };

            if (control is View)
            {
                ((View)control).HorizontalOptions = LayoutOptions.FillAndExpand;
                ((View)control).VerticalOptions = LayoutOptions.FillAndExpand;

                this.Children.Add(title);
                this.Children.Add(star);
                this.Children.Add((View)control);
            }

        }


    }
}
