using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public class UcBrowseImagesPage : UcCarouselPage
    {
        private List<ContentPage> _pages = new List<ContentPage>();

        public UcBrowseImagesPage(IEnumerable<ImageSource> images, int selectedIndex = 0)
        {
            foreach (ImageSource image in images)
            {
                UcShowImagePage page = new UcShowImagePage(image);

                this._pages.Add(page);
                this.Children.Add(page);
            }

            this.CurrentPageChanged += new EventHandler((sender, e) =>
            {
                setTitle();
            });

            this.CurrentPage = _pages[selectedIndex];

            setTitle();
        }

        private void setTitle()
        {
            this.Title = $"{_pages.IndexOf(CurrentPage) + 1}/{_pages.Count()}";
        }

        public override string GetTitle()
        {
            return null;
        }

        public class UcShowImagePage : ContentPage
        {

            public UcShowImagePage(ImageSource imageSource)
            {
                BackgroundColor = Color.Black;

                UcZoomImage image = new UcZoomImage()
                {
                    //HorizontalOptions = LayoutOptions.FillAndExpand,
                    //VerticalOptions = LayoutOptions.FillAndExpand,
                    //WidthRequest = 5000,
                    //HeightRequest = 5000,
                    Source = imageSource,
                    Aspect = Aspect.AspectFit,
                };

                //Content = image;

                Content = new ScrollView()
                {
                    Content = image,
                    //WidthRequest = 5000,
                    //HeightRequest = 5000,
                    //HorizontalOptions = LayoutOptions.FillAndExpand,
                    //VerticalOptions = LayoutOptions.FillAndExpand,
                };
            }
        }

    }
}
