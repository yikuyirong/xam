using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Cells
{
    public partial class UcIHsLabelValueCell : ViewCell
    {
        public UcIHsLabelValueCell()
        {
            StackLayout layout = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 5, Padding = new Thickness(5) };

            Label label = new Xamarin.Forms.Label() { FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)), HorizontalOptions = LayoutOptions.Start,VerticalOptions = LayoutOptions.End };

            label.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding() { Source = IHsLabelValue, Path = "Label" });
            label.SetBinding(Xamarin.Forms.Label.TextColorProperty, new Binding() { Source = IHsLabelValue, Path = "LabelColor" });

            Label value = new Xamarin.Forms.Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Italic,
                HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.End
            };

            value.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding() { Source = IHsLabelValue, Path = "Value" });
            value.SetBinding(Xamarin.Forms.Label.TextColorProperty, new Binding() { Source = IHsLabelValue, Path = "ValueColor" });

            layout.Children.Add(label);
            layout.Children.Add(value);

            View = layout;
        }

        #region 绑定属性


        public static readonly BindableProperty IHsLabelValueProperty =
          BindableProperty.Create("IHsLabelValue", typeof(IHsLabelValue), typeof(UcIHsLabelValueCell), null);

        public IHsLabelValue IHsLabelValue
        {
            get { return (IHsLabelValue)GetValue(IHsLabelValueProperty); }
            set { SetValue(IHsLabelValueProperty, value); }
        }

        #endregion 

    }
}
