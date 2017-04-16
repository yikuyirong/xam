using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Extentsions
{
    public static class ExceptionExtension
    {
        public static string GetMessage(this Exception e)
        {
            StringBuilder sb = new StringBuilder();
            if (e.InnerException != null)
            {
                sb.Append(e.InnerException.GetMessage());
            }
            else
            {
                sb.Append($"【{e.Source}】{e.Message}");
            }

            return sb.ToString();
        }
    }
}
