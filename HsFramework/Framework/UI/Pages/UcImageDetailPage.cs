using Hungsum.Framework.Utilities;
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Extentsions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Cells;

using Plugin.Media;
using Plugin.Media.Abstractions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public class UcImageDetailPage : UcDJAnnexListPage<HsImage>
    {
        public UcImageDetailPage(string title) : base(title)
        {
            this.liveview.RowHeight = 100;
        }

        protected override IEnumerable<HsActionKey> onCreateActionKeys()
        {
            return new List<HsActionKey>()
            {
                MenuItemKeys.UserDo1.SetLabel("拍照"),
                MenuItemKeys.UserDo2.SetLabel("相册")
            };
        }

        protected override DataTemplate getDataTemplete()
        {
            return new DataTemplate(() =>
            {
                UcImageCell cell = new UcImageCell();

                cell.BindingContextChanged += new EventHandler((sender, e) =>
                {
                    if (AllowEdit)
                    {
                        cell.ContextActions.Add(new MenuItem()
                        {
                            Text = "删除",
                            Command = this,
                            CommandParameter = new HsCommandParams(MenuItemKeys.删除, cell.HsImageData),
                            IsDestructive = true
                        });
                    }

                });

                cell.SetBinding(UcImageCell.HsImageDataProperty, new Binding("."));

                return cell;
            });
        }

        protected override async void itemClick(HsImage item)
        {
            try
            {
                //await Navigation.PushAsync(new UcBrowseImagesPage(datas.Select(r => r.ImageData), datas.IndexOf(item)));

                await Navigation.PushAsync(new UcBrowseImagesPage.UcShowImagePage(item.ImageData));

                //await Navigation.PushAsync(new UcBrowseImagesPage.FullScreenImagePage(item.ImageData, "Test", 0, 1));

            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
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

                IPlatformExtension pe = DependencyService.Get<IPlatformExtension>(DependencyFetchTarget.GlobalInstance);

                string[] cachePath = new string[] { "Cache", "Images" };

                //获取图像哈希集合
                List<HsLabelValue> items = await GetWSUtil().GetSysImageHashDatas(GetLoginData().ProgressId, Djlx, DjId);

                //逐幅获取图像，如果存在本地缓存则使用本地图片。
                foreach (HsLabelValue item in items)
                {
                    //图片的Hash值
                    string hashData = item.GetValueByLabel("HashData");

                    string imageDataString = await pe.ReadTextFile(hashData, cachePath);

                    //本地缓存中不存在文件，则从网络中获取，并写入本地。
                    if (string.IsNullOrWhiteSpace(imageDataString))
                    {
                        string result = await GetWSUtil().GetSysImageById(GetLoginData().ProgressId, item.GetValueByLabel("ImageId"));

                        XElement xData = XElement.Parse(result);

                        imageDataString = xData.GetFirstElementValue("Data");

                        await pe.WriteTextFile(hashData, imageDataString, cachePath);                    
                    }

                    datas.Add(new HsImage(imageDataString) { HashData = hashData });
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

        public override async Task UpdateItems()
        {
            if (string.IsNullOrWhiteSpace(Djlx))
            {
                throw new HsException($"属性Djlx或未定义");
            }

            if (string.IsNullOrWhiteSpace(DjId))
            {
                return;
            }

            int needDeleteMaxId = int.MaxValue;

            SysWSUtil util = GetWSUtil();

            LoginData loginData = GetLoginData();


            foreach (HsImage image in this.datas)
            {
                if (string.IsNullOrWhiteSpace(image.HashData))
                {
                    needDeleteMaxId = Math.Min(needDeleteMaxId, await util.UpdateSysImage(loginData.ProgressId, Djlx, DjId, image.ImageDataString));
                }
                else
                {
                    needDeleteMaxId = Math.Min(needDeleteMaxId, await util.UpdateSysImageByHashData(loginData.ProgressId, Djlx, DjId, image.HashData));
                }
            }

            await util.DeleteSysImages(loginData.ProgressId, Djlx, DjId, needDeleteMaxId);
        }

        private async void _takePhoto()
        {
            try
            {
                //初始化
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    throw new HsException($"未找到可用的摄像头");
                }

                using (MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    AllowCropping = true,

                    Directory = "IMG_TEMP",
                    Name = $"{Guid.NewGuid().ToString("N")}.jpg"
                }))
                {
                    if (file != null)
                    {
                        using (Stream stream = file.GetStream())
                        {
                            HsImage imageData = new HsImage(stream);

                            this.datas.Add(imageData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private async void _selectPhoto()
        {
            try
            {
                //初始化
                await CrossMedia.Current.Initialize();

                using (MediaFile file = await CrossMedia.Current.PickPhotoAsync())
                {
                    if (file != null)
                    {
                        HsImage imageData = new HsImage(file.GetStream());

                        this.datas.Add(imageData);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected override void callAction(HsActionKey actionKey, object data)
        {
            if (actionKey == MenuItemKeys.UserDo1)
            {
                this._takePhoto();
            }
            else if (actionKey == MenuItemKeys.UserDo2)
            {
                this._selectPhoto();
            }
            else if (actionKey == MenuItemKeys.删除)
            {
                this.datas.Remove(data as HsImage);
            }
        }

        #region IControlValue


        public override string ControlType => Views.ControlType.ImageBean;

        #endregion

    }
}
