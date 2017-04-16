using CoreGraphics;
using Foundation;
using Hungsum.Extensions;
using Hungsum.Framework.Models;
using Hungsum.iOS;

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UIKit;

using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(IOSPlatformExtension))]
namespace Hungsum.iOS
{
    public class IOSPlatformExtension : IPlatformExtension
    {

        #region ReduceImage

        public Tuple<System.Drawing.Size, byte[]> ReduceImage(byte[] datas, int maxWidth)
        {
            UIImage originalImage = ImageFromByteArray(datas);

            //原图比较小，不用缩小
            if (originalImage.Size.Width <= maxWidth)
            {
                return new Tuple<System.Drawing.Size, byte[]>(
                    new System.Drawing.Size()
                    {
                        Width = (int)originalImage.Size.Width,
                        Height = (int)originalImage.Size.Height
                    }, datas);
            }
            else //按比例缩小
            {
                int width = maxWidth;
                int height = (int)(width * originalImage.Size.Height / originalImage.Size.Width);

                using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 width, height, 8,
                                                 (4 * width), CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
                {

                    RectangleF imageRect = new RectangleF(0, 0, width, height);

                    // draw the image
                    context.DrawImage(imageRect, originalImage.CGImage);

                    UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, originalImage.Orientation);

                    // save the image as a jpeg
                    return new Tuple<System.Drawing.Size, byte[]>(
                        new System.Drawing.Size(width, height),
                        resizedImage.AsJPEG().ToArray());
                }

            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            //
            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch
            {
                return null;
            }

            return image;
        }

        #endregion

        #region 文件操作

        private string _getAppDataFolder()
        {
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string localAppData = Path.Combine(documents, "..", "Library");

            //return localAppData;

            return documents;
        }

        #region 读文件

        /// <summary>
        /// 读文本文件，如果文件不存在则返回空。
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="paths"></param>
        public async Task<string> ReadTextFile(string filename, params string[] paths)
        {
            byte[] buffer = await ReadFile(filename, paths);

            return buffer != null ? Encoding.UTF8.GetString(buffer) : null;
        }

        public async Task<byte[]> ReadFile(string filename, params string[] paths)
        {
            //获取文件全名
            filename = Path.Combine(_getAppDataFolder(), Path.Combine(paths), filename);

            FileInfo fi = new FileInfo(filename);

            if (fi.Exists)
            {

                using (FileStream fs = fi.OpenRead())
                {
                    byte[] buffer = new byte[fs.Length];

                    await fs.ReadAsync(buffer, 0, buffer.Length);

                    return buffer;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 写文件

        /// <summary>
        /// 写文本文件，如果文件存在则覆盖之。
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="contents"></param>
        /// <param name="paths"></param>
        public async Task WriteTextFile(string filename, string contents, params string[] paths)
        {
            await WriteFile(filename, Encoding.UTF8.GetBytes(contents), 2, paths);
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="datas">数据</param>
        /// <param name="filemode">
        ///  CreateNew = 1,
        ///  Create = 2,
        ///  Open = 3,
        ///  OpenOrCreate = 4,
        ///  Truncate = 5,
        ///  Append = 6
        /// </param>
        /// <param name="paths"></param>
        public async Task WriteFile(string filename, byte[] datas, int filemode, params string[] paths)
        {
            //获取文件全名
            filename = Path.Combine(_getAppDataFolder(), Path.Combine(paths), filename);

            FileInfo fi = new FileInfo(filename);

            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            using (FileStream fs = fi.Exists ? fi.Open((FileMode)filemode) : fi.Create())
            {
                await fs.WriteAsync(datas, 0, datas.Length);
            }
        }

        #endregion

        #region 打开文件

        public void CallFile(string filename, params string[] paths)
        {
            //获取文件全名
            filename = Path.Combine(_getAppDataFolder(), Path.Combine(paths), filename);

            if (File.Exists(filename))
            {
                NSUrl url = new NSUrl(filename, true);

                CallFile(url);
            }
        }

        public void CallFile(NSUrl fileUrl)
        {
            var docControl = UIDocumentInteractionController.FromUrl(fileUrl);

            var window = UIApplication.SharedApplication.KeyWindow;
            var subViews = window.Subviews;
            var lastView = subViews.Last();
            var frame = lastView.Frame;

            docControl.PresentOpenInMenu(frame, lastView, true);

        }

        #endregion

        public bool FileExist(string filename, params string[] paths)
        {
            //获取文件全名
            filename = Path.Combine(_getAppDataFolder(), Path.Combine(paths), filename);

            return File.Exists(filename);
        }

        public Task<long> DirectoryDelete(params string[] paths)
        {
            //获取文件全名
            string dirName = Path.Combine(_getAppDataFolder(), Path.Combine(paths));

            DirectoryInfo di = new DirectoryInfo(dirName);

            long size = 0;

            if (di.Exists)
            {
                size = di.GetTotolSize();

                di.Delete(true);
            }

            return Task.FromResult(size);

        }

        #endregion


    }
}
