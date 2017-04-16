using Hungsum.Framework.Assets;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Views
{
    public class UcShortCut : StackLayout
    {

        public event EventHandler<HsEventArgs<IHsLabelValue>> Click;

        public UcShortCut(IHsLabelValue item,string icon)
        {

            this.Orientation = StackOrientation.Vertical;

            AbsoluteLayout layout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            Image image = new Image()
            {
                WidthRequest = 60,
                HeightRequest = 80,
                Aspect = Aspect.AspectFit
            }; //.AspectFit };

            string iconResName = $"Hungsum.Framework.Assets.Imgs.{(icon == null ? "icon_1" : icon)}.png";

            Assembly assembly = typeof(Assets.HsImage).GetTypeInfo().Assembly;
            ManifestResourceInfo mri = assembly.GetManifestResourceInfo(iconResName);
            if (mri == null)
            {
                iconResName = $"Hungsum.Framework.Assets.Imgs.icon_1.png";
            }

            image.Source = ImageSource.FromResource(iconResName, typeof(Assets.HsImage));
            image.Aspect = Aspect.AspectFit;


            AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);

            Button button = new Button(); //{ BackgroundColor = Color.Purple, Opacity = 0.5 };

            AbsoluteLayout.SetLayoutBounds(button, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(button, AbsoluteLayoutFlags.All);

            button.Clicked += new EventHandler((sender, e) =>
            {
                this.Click?.Invoke(this, new HsEventArgs<IHsLabelValue>() { Data = item });
            });

            layout.Children.Add(image);
            layout.Children.Add(button);

            Label label = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                Text = item.Label,
                LineBreakMode = LineBreakMode.NoWrap,
                HorizontalOptions = LayoutOptions.Center
            };


            this.Children.Add(layout);
            this.Children.Add(label);

        }
    }
}
