using FormsPlugin.Iconize;
using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Cells
{
    public partial class UcHsLabelValueCell : ViewCell
    {
        private StackLayout layout;

        public UcHsLabelValueCell()
        {

            layout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = new Thickness(13)
            };

            StackLayout labelLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 2
            };

            Label label = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            label.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding() { Source = HsLabelValue, Path = "Label" });
            label.SetBinding(Xamarin.Forms.Label.TextColorProperty, new Binding() { Source = HsLabelValue, Path = "LabelColor" });

            Label value = new Label() { FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), FontAttributes = FontAttributes.Italic, HorizontalOptions = LayoutOptions.StartAndExpand };

            value.SetBinding(Xamarin.Forms.Label.TextProperty, new Binding() { Source = HsLabelValue, Path = "Value" });
            value.SetBinding(Xamarin.Forms.Label.TextColorProperty, new Binding() { Source = HsLabelValue, Path = "ValueColor" });

            IconLabel showDatail = new IconLabel()
            {
                Text = "ion-chevron-right",
                VerticalOptions = LayoutOptions.Center
            };

            showDatail.SetBinding(Xamarin.Forms.Label.IsVisibleProperty, new Binding() { Source = HsLabelValue, Path = "ItemShowDetailImage" });

            labelLayout.Children.Add(label);
            labelLayout.Children.Add(value);

            layout.Children.Add(labelLayout);
            layout.Children.Add(showDatail);

            

            View = layout;
        }

        public int RequestHeight
        {
            get
            {
                return (int)layout.HeightRequest;
            }
        }

        #region 绑定属性


        public static readonly BindableProperty HsLabelValueProperty =
          BindableProperty.Create("HsLabelValue", typeof(HsLabelValue), typeof(UcHsLabelValueCell), null);

        public HsLabelValue HsLabelValue
        {
            get { return (HsLabelValue)GetValue(HsLabelValueProperty); }
            set { SetValue(HsLabelValueProperty, value); }
        }

        #endregion 

    }
}
