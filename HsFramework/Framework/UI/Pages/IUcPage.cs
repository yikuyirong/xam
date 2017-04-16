using Hungsum.Framework.Utilities;
using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hungsum.Framework.UI.Pages
{
    public interface IUcPage : ICommand
    {
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <returns></returns>
        string GetTitle();

        /// <summary>
        /// 获取背景图
        /// </summary>
        /// <returns></returns>
        string GetBackgroundImage();

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        LoginData GetLoginData();

        /// <summary>
        /// 获取网络组件
        /// </summary>
        /// <returns></returns>
        SysWSUtil GetWSUtil();

        #region Show message

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="error"></param>
        void ShowError(string error);

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="text"></param>
        void ShowInformation(string text);

        /// <summary>
        /// 显示等待框
        /// </summary>
        /// <param name="text"></param>
        void ShowLoading(string text);

        /// <summary>
        /// 隐藏等待框
        /// </summary>
        void HideLoading();

        #endregion

        #region lifeCycle

        void OnPushed();

        void OnPoped();
        
        #endregion
    }
}
