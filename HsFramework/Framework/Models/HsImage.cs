using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Utilities;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.Models
{
    public class HsImage
    {
        private const byte ENDIAN_LITTLE = 0;

        private const byte ENDIAN_BIG = 1;

        private const int PIXEL = 0;

        private const int JPG = 1;

        private const int PNG = 2;

        private const int METALEN = 128;


        public string ImageId { get; set; }

        private ImageSource _imageData;

        public ImageSource ImageData => _imageData;

        private string _imageDataString;

        /// <summary>
        /// 图像字符串数据
        /// </summary>
        public string ImageDataString => _imageDataString;

        /// <summary>
        /// 图像Hash值
        /// </summary>
        public string HashData { get; set; }

        public HsImage(string imageDataString)
        {
            if (imageDataString.Split(',').Length > 1)
            {
                throw new HsException("无法识别的旧的图像格式");
            }

            byte[] buffers = HsBase64.FromBase64ToBytes(imageDataString);

            byte[] datas = new byte[buffers.Length - METALEN];

            using (MemoryStream bufferStream = new MemoryStream(buffers))
            {
                int endian = buffers[0];

                using (BinaryReader br = new BinaryReader(bufferStream, endian == ENDIAN_BIG ? Encoding.BigEndianUnicode : Encoding.Unicode))
                {
                    br.ReadByte(); //越过第一位

                    int format = br.ReadInt32(); //读取2-5位

                    if (format == PIXEL)
                    {
                        throw new HsException("不支持PIXEL图像格式");
                    }

                    bufferStream.Seek(METALEN, SeekOrigin.Begin);

                    bufferStream.Read(datas, 0, datas.Length);
                }
            }

            this._imageDataString = imageDataString;

            this._imageData = ImageSource.FromStream(() =>
            {
                return new MemoryStream(datas);
            });
        }

        public HsImage(Stream imageStream)
        {
            byte[] datas = new byte[imageStream.Length];

            imageStream.Seek(0, SeekOrigin.Begin);

            imageStream.Read(datas, 0, datas.Length);


            #region 获取图像大小

            IPlatformExtension platformExtension = DependencyService.Get<IPlatformExtension>(DependencyFetchTarget.GlobalInstance);

            

            Tuple<System.Drawing.Size,byte[]> imageData = platformExtension.ReduceImage(datas,800);

            System.Drawing.Size size = imageData.Item1;
            datas = imageData.Item2;

            #endregion

            #region 记录元数据

            byte[] metabs = new byte[METALEN];

            using (MemoryStream ms = new MemoryStream(metabs))
            {
                //以小端方式记录元数据。
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.Unicode))
                {
                    bw.Write(ENDIAN_LITTLE); //记录第一位
                    bw.Write(JPG); //记录2-5位
                    bw.Write(size.Width); //记录6-9位
                    bw.Write(size.Height); //记录10-13位
                }
            }

            #endregion

            _imageData = ImageSource.FromStream(() =>
            {
                return new MemoryStream(datas);
            });

            _imageDataString = HsBase64.ToBase64(metabs.Concat(datas).ToArray());

        }

    }
}
