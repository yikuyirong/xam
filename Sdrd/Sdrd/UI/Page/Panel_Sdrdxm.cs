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
    public class Panel_Sdrdxm : Panel_DJ
    {

        private UcAutoCompleteInput _ucKh;

        private UcTextInput _ucXmmc;

        private UcTextInput _ucXmly;

        private UcDateInput _ucRq;

        private UcTextInput _ucLxr;

        private UcTextInput _ucZy;

        private UcCheckedInput _ucXmjd;

        private UcTextInput _ucBz;

        public Panel_Sdrdxm(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = SdrdDjlx.RDXM;

            titleSaved = "项目维护";
        }

        protected override void onCreateMainItems()
        {
            this._ucKh = new UcAutoCompleteInput(SdrdDjlx.RDKH);
            this._ucKh.CName = "客户信息";
            this._ucKh.AllowEmpty = false;
            this.controls.Add(this._ucKh);

            this._ucXmmc = new UcTextInput();
            this._ucXmmc.CName = "项目名称";
            this._ucXmmc.AllowEmpty = false;
            this.controls.Add(this._ucXmmc);

            this._ucXmly = new UcTextInput();
            this._ucXmly.CName = "项目来源";
            this.controls.Add(this._ucXmly);

            this._ucRq = new UcDateInput();
            this._ucRq.CName = "日期";
            this._ucRq.AllowEmpty = false;
            this.controls.Add(this._ucRq);

            this._ucLxr = new UcTextInput();
            this._ucLxr.CName = "我方联系人";
            this.controls.Add(this._ucLxr);

            this._ucZy = new UcTextInput();
            this._ucZy.CName = "摘要";
            this.controls.Add(this._ucZy);

            this._ucXmjd = new UcCheckedInput("0,联系中;10,进行中;15,暂停;20,合作;30,未合作;35,用户终止", "");
            this._ucXmjd.CName = "项目进度";
            this._ucXmjd.AllowEmpty = false;
            this.controls.Add(this._ucXmjd);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("XmId");
            this._ucXmmc.ControlValue = data.GetValueByLabel("Xmmc");
            this._ucXmly.ControlValue = data.GetValueByLabel("Xmly");
            this._ucKh.ControlValue = data.GetValueByLabel("KhId") + "," + data.GetValueByLabel("Khmc");
            this._ucKh.AllowEdit = false;
            this._ucRq.ControlValue = data.GetValueByLabel("Rq");
            this._ucLxr.ControlValue = data.GetValueByLabel("Lxr");
            this._ucZy.ControlValue = data.GetValueByLabel("Zy");
            this._ucXmjd.ControlValue = data.GetValueByLabel("Xmjd");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");

        }

        protected override async Task<string> update()
        {
            this.uniqueId = await ((SdrdWSUtil)GetWSUtil()).UpdateXm(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucXmmc.ControlValue,
                this._ucXmly.ControlValue,
                this._ucKh.ControlValue,
                this._ucRq.ControlValue,
                this._ucLxr.ControlValue,
                this._ucZy.ControlValue,
                this._ucXmjd.ControlValue,
                this._ucBz.ControlValue,
                this.iNewRecode);

            return "项目更新成功。";
        }
    }
}
