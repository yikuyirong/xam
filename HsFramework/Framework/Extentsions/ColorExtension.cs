using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Extentsions
{
    public static class ColorExtension
    {

        public static Xamarin.Forms.Color ToXamarinColor(this int color)
        {
            byte[] argb = BitConverter.GetBytes(color);

            return Xamarin.Forms.Color.FromRgb(argb[2], argb[1], argb[0]);
        }
    }
}
