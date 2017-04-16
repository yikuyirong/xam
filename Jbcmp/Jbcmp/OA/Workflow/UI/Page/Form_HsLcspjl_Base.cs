using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.OA.Utilities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.OA.Workflow.UI.Page
{
    public abstract class Form_HsLcspjl_Base : UcDJListPage
    {
        public Form_HsLcspjl_Base()
        {
            this.uniqueIdField = "JlId";

        }

        public override string GetTitle()
        {
            return "审批记录";
        }


    }
}
