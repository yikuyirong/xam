using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Extentsions
{
    public static class NumberExtension
    {
        public static string GetFileSizeString(this long filesize, string dw = "B,K,M,G,T")
        {
            string[] dws = dw.Split(',');

            for (int i = 1; i <= dw.Length; i++)
            {
                if (filesize < Math.Pow(1024, i))
                {
                    return $"{(filesize / Math.Pow(1024, (i - 1))).ToString("0.00")}{dws[i - 1]}";
                }
            }
            return string.Empty;
        }
    }
}
