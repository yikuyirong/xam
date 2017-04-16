using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Hungsum.Framework.Utilities
{
    public class HsGZip
    {

        public static string CompressString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rawString.ToString());
                byte[] zippedData = compress(rawData);
                
                return Convert.ToBase64String(zippedData);
            }
        }

        public static string DecompressString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
            {
                return "";
            }
            else
            {
                //将base64空格替换为+通过POST调用此方法时传入的base64字符串中，+会被空格替换，所以这里要换回来。
                zippedString = zippedString.Replace(" ", "+");

                byte[] zippedData = Convert.FromBase64String(zippedString.ToString());

                byte[] ds = decompress(zippedData);

                return (string)(Encoding.UTF8.GetString(ds,0,ds.Length));
            }
        }

        public static byte[] CompressBytes(byte[] rawBytes)
        {
            return compress(rawBytes);
        }

        public static byte[] DecompressBytes(byte[] zippedBytes)
        {
            return decompress(zippedBytes);
        }

        /// <summary>  
        /// GZip压缩  
        /// </summary>  
        /// <param name="rawData"></param>  
        /// <returns></returns>  
        protected static byte[] compress(byte[] rawData)
        {
            MemoryStream ms = new MemoryStream();
            using (GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true))
            {
                compressedzipStream.Write(rawData, 0, rawData.Length);
            }
            return ms.ToArray();
        }

        /// <summary>  
        /// ZIP解压  
        /// </summary>  
        /// <param name="zippedData"></param>  
        /// <returns></returns>  
        protected static byte[] decompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);

            MemoryStream outBuffer = new MemoryStream();

            using (GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress))
            {
                byte[] block = new byte[1024];
                while (true)
                {
                    int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                    if (bytesRead <= 0)
                        break;
                    else
                        outBuffer.Write(block, 0, bytesRead);
                }
            }

            return outBuffer.ToArray();
        }
    }
}

