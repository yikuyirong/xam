using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;
using Hungsum.OA.Utilities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.OA.Workflow.UI.Page
{
    public class Form_HsLcspjl_Show : Form_HsLcspjl_Base
    {
        //private HsLabelValue _currentLcspjl;
        private string _djlx, _djId;

        public Form_HsLcspjl_Show(HsLabelValue item) : this(item.GetValueByLabel("Djlx"), item.GetValueByLabel("DjId"))
        {
        }

        public Form_HsLcspjl_Show(string djlx, string djId) : base()
        {
            this._djlx = djlx;
            this._djId = djId;
        }


        public override string GetTitle()
        {
            return "审批记录";
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((HsOAWSUtil)GetWSUtil()).ShowHsLcspjls(GetLoginData().ProgressId,
                this._djlx, this._djId);
        }

        protected override async Task modifyItem(HsLabelValue item)
        {
            //base.modifyItem(item);
        }


        //protected async override void modifyItem(HsLabelValue item)
        //{
        //    try
        //    {
        //        Panel_Sdrdkh panel = new Panel_Sdrdkh(item);

        //        panel.UpdateComplete += new EventHandler((sender, e) =>
        //        {
        //            this.callRetrieve(false);
        //        });

        //        await Navigation.PushAsync(panel);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowError(ex.Message);
        //    }
        //}

    }
}
