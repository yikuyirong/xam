using Acr.UserDialogs;

using System;
using System.Drawing;

namespace Hungsum.Framework.Utilities
{
    public static class HsToastHelper
    {
        public static void ShowError(string err)
        {
            UserDialogs.Instance.Toast(
                new ToastConfig(err)
                {
                    BackgroundColor = Color.Red,
                    MessageTextColor = Color.White,
                    Duration = new TimeSpan(0, 0, 2)
                });
        }

        public static void ShowSuccess(string message = "处理成功")
        {
            UserDialogs.Instance.Toast(
                new ToastConfig(message)
                {
                    BackgroundColor = Color.DarkGreen,
                    MessageTextColor = Color.White,
                    Duration = new TimeSpan(0, 0, 2)
                });
        }

        public static void ShowLoading(string message = "载入中")
        {
            UserDialogs.Instance.ShowLoading(message, MaskType.Black);
        }

        public static void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
