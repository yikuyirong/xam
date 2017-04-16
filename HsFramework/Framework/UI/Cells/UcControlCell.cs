using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Cells
{
    public class UcControlCell<T> : ViewCell where T : View, IControlValue
    {
        public UcControlCell(T control)
        {
            StackLayout layout = new StackLayout()
            {
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal
            };

            Label title = new Label() { Text = control.CName, MinimumWidthRequest = 100, VerticalOptions = LayoutOptions.CenterAndExpand };

            Label star = new Label() { Text = "*", TextColor = control.AllowEmpty ? Color.Transparent : Color.Red, VerticalOptions = LayoutOptions.CenterAndExpand };

            control.HorizontalOptions = LayoutOptions.FillAndExpand;
            control.VerticalOptions = LayoutOptions.FillAndExpand;

            layout.Children.Add(title);
            layout.Children.Add(star);
            layout.Children.Add(control);

            View = layout;

        }
    }
}
