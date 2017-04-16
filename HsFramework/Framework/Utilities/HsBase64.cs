using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hungsum.Framework.Utilities
{
    public class HsBase64
    {
        /// <summary>
        /// 将字符串进行Base64编码，使用操作系统默认字符集
        /// </summary>
        /// <param name="value">未编码字符串</param>
        /// <returns>编码后字符串</returns>
        //public static string ToBase64(string value)
        //{
        //    return ToBase64(value, Encoding.Default);
        //}

        /// <summary>
        /// 将字符串进行Base64编码，自定义字符集
        /// </summary>
        /// <param name="value">未编码字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>编码后字符串</returns>
        public static string ToBase64(string value, Encoding encoding)
        {
            return ToBase64(encoding.GetBytes(value));
        }

        public static string ToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 将Base64编码字符串进行解码，使用操作系统默认字符集
        /// </summary>
        /// <param name="value">编码后字符串</param>
        /// <returns>未编码字符串</returns>
        //public static string FromBase64(string value)
        //{
        //    return FromBase64(value, Encoding.Default);
        //}

        /// <summary>
        /// 将Base64编码字符串进行解码，自定义字符集
        /// </summary>
        /// <param name="value">编码后字符串</param>
        /// <param name="value">字符集</param>
        /// <returns>未编码字符串</returns>
        public static string FromBase64(string value, Encoding encoding)
        {
            byte[] bs = FromBase64ToBytes(value);

            return encoding.GetString(bs, 0, bs.Length);
        }

        public static byte[] FromBase64ToBytes(string value)
        {
            return Convert.FromBase64String(value);
        }
    }
}
