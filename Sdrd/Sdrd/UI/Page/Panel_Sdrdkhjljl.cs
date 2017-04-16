using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.Sdrd.Models;
using Hungsum.Sdrd.Utilities;

using System.Threading.Tasks;

namespace Hungsum.Sdrd.UI.Page
{
    public class Panel_Sdrdkhjljl : UcDJPage
    {
        private UcAutoCompleteInput _ucXm;

        private UcAutoCompleteInput _ucLxr;

        private UcDateInput _ucJlrq;

        private UcCheckedInput _ucJllx;

        private UcCheckedInput _ucJlpj;

        private UcTextArea _ucJlzy;

        private UcTextInput _ucBz;

        public Panel_Sdrdkhjljl(HsLabelValue item = null) :
            base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            titleSaved = "客户交流记录维护";

            this.Djlx = SdrdDjlx.RDKHJLJL;

        }

        protected override void onCreateMainItems()
        {
            this._ucLxr = new UcAutoCompleteInput(SdrdDjlx.RDLXR);
            this._ucLxr.CName = "客户信息";
            this._ucLxr.AllowEmpty = false;
            this.controls.Add(this._ucLxr);

            this._ucXm = new UcAutoCompleteInput(SdrdDjlx.RDXM, "1");
            this._ucXm.CName = "项目信息";
            this.controls.Add(this._ucXm);

            this._ucJlrq = new UcDateInput();
            this._ucJlrq.CName = "交流日期";
            this._ucJlrq.Flag = UcDateInput.NOW;
            this._ucJlrq.AllowEmpty = false;
            this.controls.Add(this._ucJlrq);

            this._ucJllx = new UcCheckedInput("0,初次拜访;1,正常回访 ;2,项目跟进;3,其他", "1");
            this._ucJllx.CName = "交流类型";
            this._ucJllx.AllowEmpty = false;
            this.controls.Add(this._ucJllx);

            this._ucJlpj = new UcCheckedInput("1,不满意;5,基本满意;10,非常满意", "5");
            this._ucJlpj.CName = "交流评价";
            this._ucJlpj.AllowEmpty = false;
            this.controls.Add(this._ucJlpj);

            this._ucJlzy = new UcTextArea();
            this._ucJlzy.CName = "交流摘要";
            this.controls.Add(this._ucJlzy);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("JlId");

            this._ucXm.ControlValue = $"{data.GetValueByLabel("XmId")},{data.GetValueByLabel("Xmmc")}";

            this._ucLxr.ControlValue = $"{data.GetValueByLabel("LxrId")},{data.GetValueByLabel("Khmc")}-{data.GetValueByLabel("LxrName")}";

            this._ucJlrq.ControlValue = data.GetValueByLabel("Jlrq");

            this._ucJllx.ControlValue = data.GetValueByLabel("Jllx");

            this._ucJlpj.ControlValue = data.GetValueByLabel("Jlpj");

            this._ucJlzy.ControlValue = data.GetValueByLabel("Jlzy");

            this._ucBz.ControlValue = data.GetValueByLabel("Bz");
        }

        protected override async Task<string> update()
        {
            this.uniqueId = await ((SdrdWSUtil)GetWSUtil()).UpdateKhjljl(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucXm.ControlValue,
                this._ucLxr.ControlValue,
                this._ucJlrq.ControlValue,
                this._ucJllx.ControlValue,
                this._ucJlpj.ControlValue,
                this._ucJlzy.ControlValue,
                this._ucBz.ControlValue,
                this.iNewRecode);

            return "更新成功。";
        }
    }
}
