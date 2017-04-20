using Hungsum.Sdrd.Utilities;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Sdrd.Models;
using Hungsum.Framework.Events;
using Hungsum.Framework.Utilities;
using Hungsum.Framework.Extentsions;
using System.Xml.Linq;

namespace Hungsum.Sdrd.UI.Page
{
    public class Panel_Sdrdhthkjl : Panel_DJ
    {
        private UcAutoCompleteInput _ucHt;

        private UcDateInput _ucJlrq;

        private UcTextInput _ucHksm;

        private UcTextInput _ucHkje;

        private UcTextInput _ucBz;

        public Panel_Sdrdhthkjl(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = SdrdDjlx.RDHTHKJL;

            titleSaved = "合同回款记录维护";
        }

        protected override void onCreateMainItems()
        {
            this._ucHt = new UcAutoCompleteInput(SdrdDjlx.RDHT);
            this._ucHt.CName = "合同信息";
            this._ucHt.AllowEmpty = true;
            this._ucHt.DataChanged += new EventHandler<HsEventArgs<string>>(async (sender, e) =>
            {
                try
                {
                    if (this.iNewRecode == 0)
                    {
                        //获取合同未回款金额。
                        string htId = this._ucHt.ControlValue;

                        if (!string.IsNullOrWhiteSpace(htId))
                        {
                            string result = await ((SdrdWSUtil)GetWSUtil()).GetHt(GetLoginData().ProgressId, htId);

                            HsLabelValue ht = XElement.Parse(result).ToHsLabelValue();

                            this._ucHkje.ControlValue = ht.GetValueByLabel("Syje");
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            });
            this.controls.Add(this._ucHt);

            this._ucJlrq = new UcDateInput() { Flag = UcDateInput.NOW };
            this._ucJlrq.CName = "合同日期";
            this._ucJlrq.AllowEmpty = false;
            this.controls.Add(this._ucJlrq);

            this._ucHksm = new UcTextInput();
            this._ucHksm.CName = "回款说明";
            this.controls.Add(this._ucHksm);

            this._ucHkje = new UcNumInput() { CanFushu = false };
            this._ucHkje.CName = "合同金额";
            this._ucHkje.AllowEmpty = false;
            this.controls.Add(this._ucHkje);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

        }


        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("JlId");
            this._ucHt.ControlValue = data.GetValueByLabel("HtId") + "," + data.GetValueByLabel("Htmc");
            this._ucHt.AllowEdit = false;
            this._ucJlrq.ControlValue = data.GetValueByLabel("Jlrq");
            this._ucHksm.ControlValue = data.GetValueByLabel("Hksm");
            this._ucHkje.ControlValue = data.GetValueByLabel("Hkje");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");

        }

        protected override async Task<string> update()
        {
            this.uniqueId = await ((SdrdWSUtil)GetWSUtil()).UpdateHthkjl(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucHt.ControlValue,
                this._ucJlrq.ControlValue,
                this._ucHksm.ControlValue,
                this._ucHkje.ControlValue,
                this._ucBz.ControlValue,
                this.iNewRecode);

            return "记录更新成功";
        }
    }
}
