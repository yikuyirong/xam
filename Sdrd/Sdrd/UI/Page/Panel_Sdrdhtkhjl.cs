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

namespace Hungsum.Sdrd.UI.Page
{
    public class Panel_Sdrdhtkhjl : Panel_DJ
    {

        private UcAutoCompleteInput _ucHt;

        private UcDateInput _ucJlrq;

        private UcTextInput _ucKhmyd;

        private UcTextInput _ucSjzlyll;

        private UcTextInput _ucXmjd;

        private UcTextInput _ucXmrcgl;

        private UcTextInput _ucBmjdxz;

        private UcTextInput _ucKzxzb;

        private UcTextInput _ucBz;

        public Panel_Sdrdhtkhjl(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = SdrdDjlx.RDHTKHJL;

            titleSaved = "合同考核维护";
        }

        protected override void onCreateMainItems()
        {
            this._ucHt = new UcAutoCompleteInput("RDHT_WKH","",true);
            this._ucHt.CName = "合同信息";
            this._ucHt.AllowEmpty = true;
            this.controls.Add(this._ucHt);

            this._ucJlrq = new UcDateInput() { Flag = UcDateInput.NOW };
            this._ucJlrq.CName = "记录日期";
            this._ucJlrq.AllowEmpty = false;
            this.controls.Add(this._ucJlrq);

            this._ucKhmyd = new UcTextInput();
            this._ucKhmyd.CName = "客户满意度";
            this.controls.Add(this._ucKhmyd);

            this._ucSjzlyll = new UcTextInput();
            this._ucSjzlyll.CName = "设计质量优良率";
            this._ucJlrq.AllowEmpty = false;
            this.controls.Add(this._ucSjzlyll);

            this._ucXmjd = new UcTextInput();
            this._ucXmjd.CName = "项目进度";
            this.controls.Add(this._ucXmjd);

            this._ucXmrcgl = new UcTextInput();
            this._ucXmrcgl.CName = "项目日常管理";
            this.controls.Add(this._ucXmrcgl);

            this._ucBmjdxz = new UcTextInput();
            this._ucBmjdxz.CName = "部门间的协作";
            this.controls.Add(this._ucBmjdxz);

            this._ucKzxzb = new UcTextInput();
            this._ucKzxzb.CName = "控制性指标";
            this.controls.Add(this._ucKzxzb);

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
            this._ucKhmyd.ControlValue = data.GetValueByLabel("Khmyd");
            this._ucSjzlyll.ControlValue = data.GetValueByLabel("Sjzlyll");
            this._ucXmjd.ControlValue = data.GetValueByLabel("Xmjd");
            this._ucXmrcgl.ControlValue = data.GetValueByLabel("Xmrcgl");
            this._ucBmjdxz.ControlValue = data.GetValueByLabel("Bmjdxz");
            this._ucKzxzb.ControlValue = data.GetValueByLabel("Kzxzb");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");

        }

        protected override async Task<string> update()
        {
            this.uniqueId = await ((SdrdWSUtil)GetWSUtil()).UpdateHtkhjl(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucHt.ControlValue,
                this._ucJlrq.ControlValue,
                this._ucKhmyd.ControlValue,
                this._ucSjzlyll.ControlValue,
                this._ucXmjd.ControlValue,
                this._ucXmrcgl.ControlValue,
                this._ucBmjdxz.ControlValue,
                this._ucKzxzb.ControlValue,
                this._ucBz.ControlValue,
                this.iNewRecode);

            return "记录更新成功";
        }
    }
}
