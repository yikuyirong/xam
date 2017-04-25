using Hungsum.Framework.Models;
using Hungsum.Framework.Extentsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Xml.Linq;

namespace Hungsum.Framework.UI.Pages
{
    public class Panel_Welcome_Base : UcContentPage
    {
        public event EventHandler UpgradeComplete;

        private bool allowCheck = false;

        protected void onUpgradeComplete()
        {
            this.UpgradeComplete?.Invoke(this, null);
        }

        public Panel_Welcome_Base(ImageSource source = null)
        {
            AbsoluteLayout rootLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };


            //底图
            Image image = new Image() { Aspect = Aspect.AspectFill };
            AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);

            if (source != null)
            {
                image.Source = source;
            }

            if (getImageSource() != null)
            {
                image.Source = getImageSource();
            }

            rootLayout.Children.Add(image);

            //上层按钮
            StackLayout controlLayout = new StackLayout();
            AbsoluteLayout.SetLayoutBounds(controlLayout, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(controlLayout, AbsoluteLayoutFlags.All);

            Button button = new Button()
            {
                Text = "检查更新",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.UserDo1),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End
            };

            controlLayout.Children.Add(button);

            rootLayout.Children.Add(controlLayout);

            Content = rootLayout;
        }

        protected virtual ImageSource getImageSource()
        {
            return null;
        }

        protected override void onInit()
        {
            base.onInit();

            this._checkIsUpgrade();

        }

        protected async void _checkIsUpgrade()
        {
            try
            {
                //获取新版本
                HsLabelValue item = await getLastestIPAInfo();

                //取版本号
                HsVersion version = HsVersion.Parse(item.GetValueByLabel("Version"));

                string upgradeUri = item.GetValueByLabel("UpgradeURI");

                //检查版本最后一位，如果是奇数表明是一般更新，偶数表明是强制更新
                IPlatformExtension pe = DependencyService.Get<IPlatformExtension>();

                HsVersion currentVersion = HsVersion.Parse(pe.GetApplicationVersion());

                if (pe != null && version > currentVersion) //存在新版本
                {
                    if (version.Type == HsVersion.EType.Force)
                    {
                        await this.DisplayAlert($"发现新版本 {version}，请立即更新", $"当前版本 {currentVersion}", "确定");

                        pe.OpenURL(upgradeUri);
                    }
                    else
                    {
                        if (await this.DisplayAlert($"发现新版本 {version}，是否更新", $"当前版本 {currentVersion}", "是", "否"))
                        {
                            pe.OpenURL(upgradeUri);
                        }
                        else
                        {
                            this.onUpgradeComplete();
                        }
                    }

                }
                else
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

        protected virtual async Task<HsLabelValue> getLastestIPAInfo()
        {
            string result = await GetWSUtil().GetIOSClientInfo();

            return XElement.Parse(result).ToHsLabelValue();
        }

        public override bool CanExecute(object parameter)
        {
            return allowCheck;
        }

        public override void Execute(object parameter)
        {
            try
            {
                HsCommandParams cp = parameter as HsCommandParams;

                if (cp != null)
                {
                    if (cp.ActionKey == SysActionKeys.UserDo1)
                    {
                        allowCheck = false;

                        this._checkIsUpgrade();
                    }
                }
                else
                {
                    base.Execute(parameter);
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
            finally
            {
                allowCheck = true;
            }
        }

    }
}
