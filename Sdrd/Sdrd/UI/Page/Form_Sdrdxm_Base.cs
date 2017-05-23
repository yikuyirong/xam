using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
	public abstract class Form_Sdrdxm_Base : Form_DJ
    {
        public Form_Sdrdxm_Base()
        {
            this.uniqueIdField = "XmId";

            this.ucBeginDate = new UcDateInput()
            {
                CName = "开始日期",
                AllowEmpty = false,
                Flag = UcDateInput.MONTHFIRST
            };

            this.ucEndDate = new UcDateInput()
            {
                CName = "结束日期",
                AllowEmpty = false,
                Flag = UcDateInput.NOW
            };

            this.ucUserSwitcher = new UcCheckedInput("0,联系中;10,进行中;15,暂停;20,合作;30,未合作;35,用户终止", "0,10,15,20,30,35")
            {
                CName = "项目进度",
                AllowEmpty = false,
            };

        }
    }
}
