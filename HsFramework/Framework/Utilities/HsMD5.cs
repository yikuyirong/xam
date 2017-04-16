using System.Text;
using xBrainLab.Security.Cryptography;

namespace Hungsum.Framework.Utilities
{
    public class HsMD5
    {
        /// <summary>
        /// MD5加密函数
        /// </summary>
        /// <param name="text">明文</param>
        /// <returns>密文</returns>
        public static string EncryptionMD5(string text)
        {
            return MD5.GetHashString(text, Encoding.UTF8);
        }

        public static string EncryptionMD5(byte[] bs)
        {
            return MD5.GetHashString(bs);
        }
    }
}
