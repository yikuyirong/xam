using Hungsum.Framework.Models;
using Hungsum.Framework.Extentsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public class Panel_Welcome_Base : UcContentPage
    {
        public event EventHandler UpgradeComplete;

        protected void onUpgradeComplete()
        {
            this.UpgradeComplete?.Invoke(this, null);
        }

        public Panel_Welcome_Base(ImageSource source = null)
        {
            Image image = new Image() { Aspect = Aspect.AspectFill };

            if (source != null)
            {
                image.Source = source;
            }

            if (getImageSource() != null)
            {
                image.Source = getImageSource();
            }

            Content = image;
        }

        protected virtual ImageSource getImageSource()
        {
            return null;
        }

        protected override async void onInit()
        {
            base.onInit();

            try
            {
                //获取新版本
                string info = await getLastestIPAInfo();

                //第一位表示版本号，第二位表示URL
                string[] infos = info.Split(';');

                //取版本号
                HsVersion version = HsVersion.Parse(infos[0]);

                string url = infos[1];

                //检查版本最后一位，如果是奇数表明是一般更新，偶数表明是强制更新
                IPlatformExtension pe = DependencyService.Get<IPlatformExtension>();

                if (pe != null && version > HsVersion.Parse(pe.GetApplicationVersion())) //存在新版本
                {
                    if (version.Type == HsVersion.EType.Force)
                    {
                        await this.DisplayAlert("发现新版本，请立即更新", "", "确定");

                        pe.OpenURL(url);
                    }
                    else
                    {
                        if (await this.DisplayAlert("发现新版本，是否更新", "", "是", "否"))
                        {
                            pe.OpenURL(url);
                        }
                        else
                        {
                            this.onUpgradeComplete();
                        }
                    }

                } else
                {
                    this.onUpgradeComplete();
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);

                this.onUpgradeComplete();
            }
        }

        protected virtual async Task<string> getLastestIPAInfo()
        {
            string result = await GetWSUtil().GetIOSClientInfo();

            return result;
        }

    }
}
