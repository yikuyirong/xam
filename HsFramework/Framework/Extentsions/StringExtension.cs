using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hungsum.Framework.Extentsions
{
    public static class StringExtension
    {
        #region 全角转换半角以及半角转换为全角


        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248

        public static string ToSBC(this string input)
        {
            // 半角转全角：
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 32)
                {
                    array[i] = (char)12288;
                    continue;
                }
                if (array[i] < 127)
                {
                    array[i] = (char)(array[i] + 65248);
                }
            }
            return new string(array);
        }

        /// <summary>
        /// 转半角的函数(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        public static string ToDBC(this string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }
        #endregion

        #region XML

        public static string GetDataByTag(this string src, string tagName)
        {
            return src.GetDataByTag(tagName, false);
        }

        public static string GetDataByTag(this string src, string tagName, bool checkExist)
        {
            if (src.IndexOf("<" + tagName + ">") < 0 || src.IndexOf("</" + tagName + ">") < 0)
            {
                if (checkExist)
                {
                    throw new HsException($"未找到【{tagName}】标记。");
                }
                else
                {
                    return null;
                }
            }

            int beginIndex = src.IndexOf("<" + tagName + ">") + ("<" + tagName + ">").Length;
            int endIndex = src.IndexOf("</" + tagName + ">");
            return src.Substring(beginIndex, endIndex - beginIndex);
        }

        #endregion

        /// <summary>
        /// 与参数中任意一个比较相等机返回真
        /// </summary>
        /// <param name="src"></param>
        /// <param name="texts"></param>
        /// <returns></returns>
        public static bool ExEquals(this string src, params string[] texts)
        {
            foreach (string text in texts)
            {
                if (text == src)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 将查询返回的xml字符串转化为HsLabelValue集合。
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<HsLabelValue> ToHsLabelValues(this string result)
        {
            List<HsLabelValue> items = new List<HsLabelValue>();

            foreach (XElement xItem in XElement.Parse(result).Elements("Item"))
            {
                items.Add(xItem.ToHsLabelValue());
            }

            return items;
        }
    }
}
