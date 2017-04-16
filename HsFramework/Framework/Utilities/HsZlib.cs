using System.IO;

using zlib;

namespace Hungsum.Framework.Utilities
{

    /// <summary>
    /// 由于netframework自带的System.IO.Compression.DeflateStream兼容性问题，此处用zlib外部类库
    /// 进行daflate压缩与解压缩，这是hsframework框架中图片的标准压缩方式，flex客户端、android客户端都遵循
    /// 此种压缩方式。
    /// </summary>
    public class HsZlib
    {
        private static int BUFFERLEN = 1024;

        public static byte[] CompressData(byte[] inData)
        {
            using(MemoryStream inms = new MemoryStream(inData))
            {
                using(MemoryStream outms = new MemoryStream())
                {
                    using(ZOutputStream zos = new ZOutputStream(outms,zlibConst.Z_DEFAULT_COMPRESSION))
                    {
                        while(true)
                        {
                            byte[] buffer = new byte[BUFFERLEN];
                    
                            int len = inms.Read(buffer,0,buffer.Length);

                            if(len < 1)
                            {
                                break;
                            }

                            zos.Write(buffer,0,len);
                        }

                        zos.Flush();

                    }
                    return outms.ToArray();
                }

            }
        }

        public static byte[] DecompressData(byte[] inData)
        {
            using (MemoryStream inms = new MemoryStream(inData))
            {
                using (MemoryStream outms = new MemoryStream())
                {
                    using (ZOutputStream zos = new ZOutputStream(outms))
                    {
                        while (true)
                        {
                            byte[] buffer = new byte[BUFFERLEN];

                            int len = inms.Read(buffer, 0, buffer.Length);

                            if (len < 1)
                            {
                                break;
                            }

                            zos.Write(buffer, 0, len);
                        }

                        zos.Flush();

                    }
                    return outms.ToArray();
                }

            }
        }
    }
}
