using Hungsum.Sdrd.Utilities;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Sdrd.UI.Page
{
    public class Panel_Sdrdkh : UcDJPage
    {
        private UcTextInput _ucKhmc;

        private UcTextInput _ucDz;

        private UcTextInput _ucYzbm;

        private UcTextInput _ucBz;

        public Panel_Sdrdkh(HsLabelValue item = null) : base(item) { }

        protected override void onInit()
        {
            base.onInit();

            titleSaved = "客户维护";
        }


        protected override void onCreateMainItems()
        {
            this._ucKhmc = new UcTextInput();
            this._ucKhmc.CName = "客户名称";
            this._ucKhmc.AllowEmpty = false;
            this.controls.Add(this._ucKhmc);

            this._ucDz = new UcTextInput();
            this._ucDz.CName = "地址";
            this.controls.Add(this._ucDz);

            this._ucYzbm = new UcTextInput();
            this._ucYzbm.CName = "邮政编码";
            this.controls.Add(this._ucYzbm);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("KhId");
            this._ucKhmc.ControlValue = data.GetValueByLabel("Khmc");
            this._ucDz.ControlValue = data.GetValueByLabel("Dz");
            this._ucYzbm.ControlValue = data.GetValueByLabel("Yzbm");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");
        }

        protected override async Task<string> update()
        {
            return await ((SdrdWSUtil)GetWSUtil()).UpdateKh(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucKhmc.ControlValue,
                this._ucDz.ControlValue,
                this._ucYzbm.ControlValue,
                this._ucDz.ControlValue,
                this.iNewRecode);
        }
    }
}
