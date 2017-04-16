using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Extentsions
{
    public static class DateTimeExtension
    {
        public static DateTime GetMonthFirst(this DateTime today)
        {
            return new DateTime(today.Year, today.Month, 1);
        }

        public static DateTime GetYearFirst(this DateTime today)
        {
            return new DateTime(today.Year, 1, 1);
        }
    }
}
