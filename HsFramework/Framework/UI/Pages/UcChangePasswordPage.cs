using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;
using Hungsum.Framework.Exceptions;
using Rg.Plugins.Popup.Services;

namespace Hungsum.Framework.UI.Pages
{
    public class UcChangePasswordPage : UcNormalPopupPage<object>
    {
        private UcPassword ucOldPassword;

        private UcPassword ucNewPassword;

        private UcPassword ucNewPassword2;

        public UcChangePasswordPage()
        {
            this.Title = "修改密码";

            ucOldPassword = new UcPassword() { CName = "原密码", AllowEmpty = false };

            ucNewPassword = new UcPassword() { CName = "新密码", AllowEmpty = false };

            ucNewPassword2 = new UcPassword() { CName = "确认密码", AllowEmpty = false };

            mainLayout.Children.Add(new UcFormItem(ucOldPassword));

            mainLayout.Children.Add(new UcFormItem(ucNewPassword));

            mainLayout.Children.Add(new UcFormItem(ucNewPassword2));

        }

        protected override async void callAction(HsActionKey actionKey, object item)
        {
            try
            {
                if (actionKey == MenuItemKeys.UserDo1)
                {
                    ucOldPassword.Validate();
                    ucNewPassword.Validate();
                    ucNewPassword2.Validate();

                    if (ucNewPassword.ControlValue != ucNewPassword2.ControlValue)
                    {
                        throw new HsException("新密码与确认密码不一致");
                    }

                    await GetWSUtil().ChangePassword(
                        GetLoginData().ProgressId,
                        ucOldPassword.ControlValue,
                        ucNewPassword.ControlValue);

                    await this.DisplayAlert("密码修改成功", "", "确定");

                    await PopupNavigation.PopAsync();
                }
                else if (actionKey == MenuItemKeys.UserDo2)
                {
                    await PopupNavigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }
    }
}
