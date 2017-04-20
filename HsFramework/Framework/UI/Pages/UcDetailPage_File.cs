using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Cells;
using Hungsum.Framework.Utilities;
using Hungsum.Framework.Extentsions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Diagnostics;

namespace Hungsum.Framework.UI.Pages
{
    public class UcDetailPage_File : UcDetailPage_Annex<HsFile>
    {
        public UcDetailPage_File(string title) : base(title)
        {
            this.liveview.RowHeight = 60;
        }

        protected override DataTemplate getDataTemplete()
        {
            return new DataTemplate(() =>
            {
                UcFileCell cell = new UcFileCell();

                cell.BindingContextChanged += new EventHandler((sender, e) =>
                {
                    cell.ContextActions.Add(new MenuItem()
                    {
                        Text = "打开",
                        Command = this,
                        CommandParameter = new HsCommandParams(SysActionKeys.UserDo1.SetLabel("打开"), cell.HsFileData)
                    });

                });

                cell.SetBinding(UcFileCell.HsFileDataProperty, new Binding("."));

                return cell;
            });
        }

        private async Task downloadAndOpen(HsFile item)
        {
            //如果文件存在与本地，则直接打开，否则先下载再打开。

            string[] cachePath = new string[] { "Cache", "Files" };

            string cachefilename = $"{item.FileHash}.{item.FileName}";

            IPlatformExtension pe = DependencyService.Get<IPlatformExtension>(DependencyFetchTarget.GlobalInstance);

            if (!pe.FileExist(cachefilename,cachePath)) //文件不存在需要下载
            {
                int index = 0;

                using (MemoryStream ms = new MemoryStream())
                {
                    while (true)
                    {
                        //下载文件...
                        HsLabelValue result = await GetWSUtil().GetSysFileSeg(GetLoginData().ProgressId, item.FileHash, index);

                        if (result == null) //文件读取完毕
                        {
                            break;
                        }

                        byte[] buffer = HsBase64.FromBase64ToBytes(result.GetValueByLabel("Data"));

                        //数据解压缩
                        buffer = HsZlib.DecompressData(buffer);

                        ms.Write(buffer, 0, buffer.Length);

                        index++;
                    }

                    byte[] fileData = ms.ToArray();

                    Debug.WriteLine(HsMD5.EncryptionMD5(fileData));

                    await pe.WriteFile(cachefilename, fileData, 2, cachePath);

                }


                //using (MemoryStream ms = new MemoryStream())
                //{

                //    await pe.WriteFile(cachefilename, ms.ToArray(), 2, cachePath);
                //}

                await downloadAndOpen(item);
            }
            else
            {
                pe.CallFile(cachefilename, cachePath);
            }
        }

        public override async Task GetItems()
        {
            try
            {
                this.isRetrieveing = true;

                if (string.IsNullOrWhiteSpace(Djlx))
                {
                    throw new HsException($"属性Djlx或未定义");
                }

                if (string.IsNullOrWhiteSpace(DjId))
                {
                    return;
                }

                //
                this.Reset();

                //获取图像哈希集合
                List<HsLabelValue> items = await GetWSUtil().GetSysFiles(GetLoginData().ProgressId, Djlx, DjId);

                foreach (HsLabelValue item in items)
                {
                    HsFile file = new HsFile()
                    {
                        FileName = item.GetValueByLabel("FileName"),
                        FileHash = item.GetValueByLabel("FileHash"),
                        FileType = item.GetValueByLabel("FileType")
                    };

                    file.FileSize = long.Parse(item.GetValueByLabel("FileSize")).GetFileSizeString();

                    string iconResName = $"Hungsum.Framework.Assets.Imgs.FileType.{(string.IsNullOrEmpty(file.FileType) ? "dat" : file.FileType.ToLower())}.png";
                    Assembly assembly = typeof(Assets.HsImage).GetTypeInfo().Assembly;
                    ManifestResourceInfo mri = assembly.GetManifestResourceInfo(iconResName);
                    if (mri == null)
                    {
                        iconResName = $"Hungsum.Framework.Assets.Imgs.FileType.dat.png";
                    }

                    file.FileIcon = ImageSource.FromResource(iconResName, assembly);

                    datas.Add(file);

                }

                hasRrtrieve = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.isRetrieveing = false;
            }
        }


        public override Task UpdateItems()
        {
            throw new NotImplementedException();
        }

        protected override async void callAction(HsActionKey actionKey, object data)
        {
            try
            {
                if (actionKey == SysActionKeys.UserDo1)
                {
                    await this.downloadAndOpen(data as HsFile);
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

        #region IControlValue

        public override string ControlType => Views.ControlType.FileBean;

        #endregion

    }
}
