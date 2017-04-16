using System;
using System.Threading.Tasks;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;

namespace Hungsum.Sdrd.UI.Page
{
    public abstract class Form_Sdrdkhjljl_Base : UcDJListPage
    {
        public Form_Sdrdkhjljl_Base()
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
            
        }


    }
}
