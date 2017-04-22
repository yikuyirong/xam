using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Jbcmp.UI.Page
{
    public class Panel_Welcome : Panel_Welcome_Base
    {
        protected override ImageSource getImageSource()
        {
            return ImageSource.FromResource("Hungsum.Jbcmp.Assets.Imgs.background_original.png");
        }

    }
}
