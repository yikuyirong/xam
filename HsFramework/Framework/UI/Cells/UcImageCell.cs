using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Cells
{
    public partial class UcImageCell : ViewCell
    {
        public UcImageCell()
        {
            StackLayout layout = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 0, Padding = new Thickness(5) };

            Image image = new Image() { Aspect = Aspect.AspectFit, HorizontalOptions = LayoutOptions.Start ,VerticalOptions = LayoutOptions.FillAndExpand};

            image.SetBinding(Image.SourceProperty, new Binding() { Source = HsImageData, Path = "ImageData" });

            layout.Children.Add(image);

            View = layout;
        }

        #region 绑定属性


        public static readonly BindableProperty HsImageDataProperty =
          BindableProperty.Create("HsImageData", typeof(HsImage), typeof(UcImageCell), null);

        public HsImage HsImageData
        {
            get { return (HsImage)GetValue(HsImageDataProperty); }
            set { SetValue(HsImageDataProperty, value); }
        }

        #endregion 

    }
}
