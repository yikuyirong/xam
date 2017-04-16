using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Cells
{
    public class HsSwitchCell : SwitchCell
    {
        public static readonly BindableProperty ValueProperty =
          BindableProperty.Create("Value", typeof(string), typeof(HsSwitchCell), null);

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}
