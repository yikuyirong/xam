
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.Jbcmp.Models;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Hungsum.Jbcmp.Utilities;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Hungsum.Jbcmp.UI.Page
{
    public class Panel_JbCgspd : UcDJPage
    {
        private UcDateInput _ucDjrq;

        private UcTextInput _uCgbt;

        private UcCheckedInput _ucSfjj;

        private UcTextInput _ucCgyy;

        private UcTextInput _ucBz;

        private UcJbCgspdDetailPage _ucDetail;

        public Panel_JbCgspd(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = JbcmpDjlx.JBCGSPD;

            titleSaved = JbcmpDjlx.JB采购审批单;
        }

        protected override void onCreateMainItems()
        {
            this._ucDjrq = new UcDateInput();
            this._ucDjrq.CName = "单据日期";
            this._ucDjrq.AllowEmpty = false;
            this.controls.Add(this._ucDjrq);

            this._uCgbt = new UcTextInput();
            this._uCgbt.CName = "采购标题";
            this._uCgbt.AllowEmpty = false;
            this.controls.Add(this._uCgbt);

            this._ucSfjj = new UcCheckedInput();
            this._ucSfjj.CName = "是否加急";
            this._ucSfjj.AllowEdit = false;
            this.controls.Add(this._ucSfjj);

            this._ucCgyy = new UcTextInput();
            this._ucCgyy.CName = "采购原因";
            this.controls.Add(this._ucCgyy);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

            this._ucDetail = new UcJbCgspdDetailPage(this.PP.detailTitle);
            this._ucDetail.CName = "采购明细";
            this._ucDetail.AllowEmpty = false;
            this.controls.Add(this._ucDetail);
            this.Children.Insert(1, this._ucDetail);

        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("DjId");
            this._ucDjrq.ControlValue = data.GetValueByLabel("Djrq");
            this._ucDjrq.AllowEdit = false;
            this._uCgbt.ControlValue = data.GetValueByLabel("Cgbt");
            this._ucSfjj.ControlValue = data.GetValueByLabel("Sfjj");
            this._ucCgyy.ControlValue = data.GetValueByLabel("Cgyy");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");
            this._ucDetail.ControlValue = data.GetValueByLabel("StrMx");

        }

        protected override async Task<string> update()
        {
            this.uniqueId = await ((JbcmpWSUtil)GetWSUtil()).UpdateJbCgspd(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucDjrq.ControlValue,
                this._uCgbt.ControlValue,
                this._ucSfjj.ControlValue,
                this._ucCgyy.ControlValue,
                this._ucBz.ControlValue,
                this._ucDetail.ControlValue,
                this.iNewRecode);

            return "项目更新成功。";
        }

        private class UcJbCgspdDetailPage : UcDJDetailPage
        {
            public UcJbCgspdDetailPage(string title) : base(title) { }

            protected override HsLabelValue createHsLabelValueFromJObject(HsLabelValue item, JObject obj)
            {
                string mc = obj.GetValue("Mc").ToString();

                string sl = obj.GetValue("Sl").ToString();

                item.Label = mc;
                item.Value = sl;

                return item;
            }

            private class Jbcgspdmx
            {
                public int MxId { get; set; }

                public int DjId { get; set; }

                public string Mc { get; set; }

                public string Xh { get; set; }

                public string Kcsl { get; set; }

                public string Sl { get; set; }

                public string Dj { get; set; }

                public string Je { get; set; }

                public string Yq { get; set; }

                public string Bz { get; set; }

            }

        }
    }
}
