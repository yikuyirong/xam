using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Models;
using Hungsum.Sdrd.Utilities;
using System.Xml.Linq;
using Xamarin.Forms;
using Hungsum.Framework.UI.Cells;
using Hungsum.Framework.UI.Views;

namespace Hungsum.Sdrd.UI.Page
{
    public abstract class Form_Sdrdhthkjl_Base : Form_DJ
    {
        public Form_Sdrdhthkjl_Base()
        {
            this.uniqueIdField = "JlId";

            this.ucBeginDate = new UcDateInput()
            {
                CName = "开始日期",
                Flag = UcDateInput.MONTHFIRST,
                AllowEmpty = false
            };

            this.ucEndDate = new UcDateInput()
            {
                CName = "结束日期",
                Flag = UcDateInput.NOW,
                AllowEmpty = false
            };

            this.ucUserSwitcher = new UcCheckedInput("0,提交;1,提交", "0,1", true)
            {
                CName = "是否提交",
                AllowEmpty = false
            };
        }

    }
}
