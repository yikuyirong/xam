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
    public class Panel_Sdrdht : Panel_DJ
    {

        private UcAutoCompleteInput _ucXm;

        private UcAutoCompleteInput _ucKh;

        private UcTextInput _ucHtbh;

        private UcTextInput _ucHtmc;

        private UcDateInput _ucHtrq;

        private UcNumInput _ucHtje;

        private UcTextInput _ucFkfs;

        private UcTextInput _ucHtqx;

        private UcTextInput _ucZytk;

        private UcTextInput _ucBz;

        public Panel_Sdrdht(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = SdrdDjlx.RDHT;

            titleSaved = "合同维护";
        }

        protected override void onCreateMainItems()
        {
            this._ucXm = new UcAutoCompleteInput(SdrdDjlx.RDKH);
            this._ucXm.CName = "项目信息";
            this._ucXm.AllowEmpty = true;
            this.controls.Add(this._ucXm);

            this._ucKh = new UcAutoCompleteInput(SdrdDjlx.RDKH);
            this._ucKh.CName = "客户信息";
            this._ucKh.AllowEmpty = true;
            this.controls.Add(this._ucKh);

            this._ucHtbh = new UcTextInput();
            this._ucHtbh.CName = "合同编号";
            this._ucHtbh.AllowEmpty = false;
            this.controls.Add(this._ucHtbh);

            this._ucHtmc = new UcTextInput();
            this._ucHtmc.CName = "合同名称";
            this._ucHtmc.AllowEmpty = false;
            this.controls.Add(this._ucHtmc);

            this._ucHtrq = new UcDateInput() { Flag = UcDateInput.NOW };
            this._ucHtrq.CName = "合同日期";
            this._ucHtrq.AllowEmpty = false;
            this.controls.Add(this._ucHtrq);

            this._ucHtje = new UcNumInput() { CanFushu = false };
            this._ucHtje.CName = "合同金额";
            this._ucHtje.AllowEmpty = false;
            this.controls.Add(this._ucHtje);

            this._ucFkfs = new UcTextInput();
            this._ucFkfs.CName = "付款方式";
            this.controls.Add(this._ucFkfs);

            this._ucHtqx = new UcTextInput();
            this._ucHtqx.CName = "合同期限";
            this.controls.Add(this._ucHtqx);

            this._ucZytk = new UcTextInput();
            this._ucZytk.CName = "主要条款";
            this.controls.Add(this._ucZytk);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("HtId");
            this._ucKh.ControlValue = data.GetValueByLabel("KhId") + "," + data.GetValueByLabel("Khmc");
            this._ucXm.ControlValue = data.GetValueByLabel("XmId") + "," + data.GetValueByLabel("Xmmc");
            this._ucHtbh.ControlValue = data.GetValueByLabel("Htbh");
            this._ucHtmc.ControlValue = data.GetValueByLabel("Htmc");
            this._ucHtrq.ControlValue = data.GetValueByLabel("Htrq");
            this._ucHtje.ControlValue = data.GetValueByLabel("Htje");
            this._ucFkfs.ControlValue = data.GetValueByLabel("Fkfs");
            this._ucHtqx.ControlValue = data.GetValueByLabel("Htqx");
            this._ucZytk.ControlValue = data.GetValueByLabel("Zytk");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");

        }

        protected override async Task<string> update()
        {
            this.uniqueId = await ((SdrdWSUtil)GetWSUtil()).UpdateHt(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucXm.ControlValue,
                this._ucKh.ControlValue,
                this._ucHtrq.ControlValue,
                this._ucHtbh.ControlValue,
                this._ucHtmc.ControlValue,
                this._ucHtje.ControlValue,
                this._ucFkfs.ControlValue,
                this._ucHtqx.ControlValue,
                this._ucZytk.ControlValue,
                this._ucBz.ControlValue,
                this.iNewRecode);

            return "合同更新成功";
        }
    }
}
