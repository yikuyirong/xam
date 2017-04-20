
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
    public class Panel_JbCgspd : Panel_DJ
    {
        private UcDateInput _ucDjrq;

        private UcTextInput _uCgbt;

        private UcCheckedInput _ucSfjj;

        private UcTextInput _ucCgyy;

        private UcTextInput _ucBz;

        private UcJbCgspdDetail _ucDetail;

        public Panel_JbCgspd(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams() { detailTitle = "采购明细" };
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

            this._ucDetail = new UcJbCgspdDetail(this.PP.detailTitle);
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

        private class UcJbCgspdDetail : UcDetailPage_Item
        {
            public UcJbCgspdDetail(string title) : base(title) { }

            protected override HsLabelValue createLabelAndValue(HsLabelValue item)
            {
                string mc = item.GetValueByLabel("Mc");

                string sl = item.GetValueByLabel("Sl");

                item.Label = mc;
                item.Value = sl;

                return item;
            }

            protected override async Task addItem()
            {
                Panel_JbCgspdDetail panel = new Panel_JbCgspdDetail();

                panel.UpdateComplete += new EventHandler<Framework.Events.HsEventArgs<object>>((sender, e) =>
                {
                    try
                    {
                        HsLabelValue data = e.Data as HsLabelValue;

                        if (data != null)
                        {
                            datas.Add(createLabelAndValue(data));
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowError(ex.Message);
                    }
                });

                await Navigation.PushAsync(panel);
            }

            protected override async Task modifyItem(HsLabelValue item)
            {
                Panel_JbCgspdDetail panel = new Panel_JbCgspdDetail(item) { AuditOnly = !AllowEdit };

                panel.UpdateComplete += new EventHandler<Framework.Events.HsEventArgs<object>>((sender, e) =>
                {
                    try
                    {
                        HsLabelValue data = e.Data as HsLabelValue;

                        if (data != null)
                        {
                            datas.Add(createLabelAndValue(data));
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowError(ex.Message);
                    }
                });

                await Navigation.PushAsync(panel);
            }

        }

        private class Panel_JbCgspdDetail : Panel_DJDetail
        {
            private UcTextInput _ucMc;

            private UcTextInput _ucXh;

            private UcTextInput _ucKcsl;

            private UcTextInput _ucSl;

            private UcTextInput _ucDj;

            private UcTextInput _ucJe;

            private UcTextInput _ucYq;

            private UcTextInput _ucBz;


            public Panel_JbCgspdDetail(HsLabelValue item = null) : base(item) { }

            protected override void onInit()
            {
                base.onInit();

                titleSaved = "采购明细";
            }

            protected override void onCreateMainItems()
            {
                this._ucMc = new UcTextInput();
                this._ucMc.CName = "名称";
                this._ucMc.AllowEmpty = false;
                this.controls.Add(this._ucMc);

                this._ucXh = new UcTextInput();
                this._ucXh.CName = "型号";
                this._ucXh.AllowEmpty = true;
                this.controls.Add(this._ucXh);

                this._ucKcsl = new UcTextInput();
                this._ucKcsl.CName = "库存数量";
                this._ucKcsl.AllowEmpty = true;
                this.controls.Add(this._ucKcsl);

                this._ucSl = new UcTextInput();
                this._ucSl.CName = "采购数量";
                this._ucSl.AllowEmpty = false;
                this.controls.Add(this._ucSl);

                this._ucDj = new UcTextInput();
                this._ucDj.CName = "单价";
                this._ucDj.AllowEmpty = true;
                this.controls.Add(this._ucDj);

                this._ucJe = new UcTextInput();
                this._ucJe.CName = "金额";
                this._ucJe.AllowEmpty = true;
                this.controls.Add(this._ucJe);

                this._ucYq = new UcTextInput();
                this._ucYq.CName = "要求";
                this._ucYq.AllowEmpty = true;
                this.controls.Add(this._ucYq);

                this._ucBz = new UcTextInput();
                this._ucBz.CName = "备注";
                this._ucBz.AllowEmpty = true;
                this.controls.Add(this._ucBz);

            }

            protected override void setData(HsLabelValue data)
            {
                this._ucMc.ControlValue = data.GetValueByLabel("Mc");
                this._ucXh.ControlValue = data.GetValueByLabel("Xh");
                this._ucKcsl.ControlValue = data.GetValueByLabel("Kcsl");
                this._ucSl.ControlValue = data.GetValueByLabel("Sl");
                this._ucDj.ControlValue = data.GetValueByLabel("Dj");
                this._ucJe.ControlValue = data.GetValueByLabel("Je");
                this._ucYq.ControlValue = data.GetValueByLabel("Yq");
                this._ucBz.ControlValue = data.GetValueByLabel("Bz");
            }

            protected override async Task<string> update()
            {
                HsLabelValue item = new HsLabelValue();

                item.AddItem(new HsLabelValue() { Label = "Mc", Value = this._ucMc.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Xh", Value = this._ucXh.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Kcsl", Value = this._ucKcsl.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Sl", Value = this._ucSl.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Dj", Value = this._ucDj.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Je", Value = this._ucJe.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Yq", Value = this._ucYq.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Bz", Value = this._ucBz.ControlValue });

                this.updateCompleteData = item;

                return await Task.FromResult("");
            }
        }
    }
}
