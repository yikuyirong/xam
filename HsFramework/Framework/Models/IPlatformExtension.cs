using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Models
{
    public interface IPlatformExtension
    {
        /// <summary>
        /// 缩小图片
        /// </summary>
        /// <param name="stream">图像数据</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <returns></returns>
        Tuple<Size,byte[]> ReduceImage(byte[] datas,int maxWidth);

        Task<string> ReadTextFile(string filename, params string[] paths);

        Task<byte[]> ReadFile(string filename, params string[] paths);

        Task WriteTextFile(string filename, string contents, params string[] paths);

        Task WriteFile(string filename, byte[] datas, int filemode, params string[] paths);

        void CallFile(string filename, params string[] paths);

        bool FileExist(string filename, params string[] paths);

        Task<long> DirectoryDelete(params string[] paths);

        string GetApplicationVersion();

		string GetApplicationVersionBuild();

        void OpenURL(string url);

    }
}
