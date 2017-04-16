using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Extentsions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 从逗号分隔的枚举值中获取逗号分割的枚举名，逗号可更换为其他分隔符。
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string GetNameStringByValueString(this Type enumType, string values, string seperateChar = ",")
        {
            if (string.IsNullOrWhiteSpace(values))
            {
                return string.Empty;
            }
            else
            {
                string[] av = values.Split(new string[] { seperateChar }, StringSplitOptions.RemoveEmptyEntries);

                //foreach (string v in av)
                //{
                //    string n = Enum.GetName(enumType, v);
                //}

                var an = av.Select(v => Enum.GetName(enumType, int.Parse(v)));

                //return "";

                return string.Join(seperateChar, an);
            }
        }
    }
}
