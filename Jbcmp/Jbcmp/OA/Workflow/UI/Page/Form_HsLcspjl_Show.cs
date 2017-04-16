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
        private HsLabelValue _currentLcspjl;

        public Form_HsLcspjl_Show(HsLabelValue item)
        {
            _currentLcspjl = item;
        }

        public override string GetTitle()
        {
            return "审批记录";
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await ((HSOAWSUtl)GetWSUtil()).ShowHsLcspjls(GetLoginData().ProgressId,
                _currentLcspjl.GetValueByLabel("Djlx"),
                _currentLcspjl.GetValueByLabel("DjId"));
        }

        protected override void modifyItem(HsLabelValue item)
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
