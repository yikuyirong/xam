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
    public class Form_Sdrdhthkjl_Query2 : Form_Sdrdhthkjl_Query
    {
        private HsLabelValue _ht;

        public Form_Sdrdhthkjl_Query2(HsLabelValue ht) : base()
        {
            this._ht = ht;

            this.ucBeginDate = null;
            this.ucEndDate = null;
            this.ucUserSwitcher = null;
        }

        public override string GetTitle()
        {
            return $"{this._ht.GetValueByLabel("Htmc")}回款记录";
        }

        

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((SdrdWSUtil)GetWSUtil()).ShowHthkjls(GetLoginData().ProgressId,
                this._ht.GetValueByLabel("HtId"));
        }
    }
}
