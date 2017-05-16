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
    public class Panel_JbBjdxpsb : Panel_DJ
    {
        private UcDateInput _ucDjrq;

		private UcTextInput _uXmmc;

		private UcCheckedInput _ucJtsp;

		private UcCheckedInput _ucFjxm;

		private UcTextInput _ucDxsm;

		private UcTextInput _ucBjyj;

		private UcTextInput _ucBjje;

        private UcTextInput _ucBz;

        private UcJbBjdxbjjls _ucDetail;

        public Panel_JbBjdxpsb(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams() { detailTitle = "报价记录" };
        }

        protected override void onInit()
        {
            base.onInit();

			this.Djlx = JbcmpDjlx.JBBJDXPSB;

			titleSaved = JbcmpDjlx.JB比价定向评审表;
        }

        protected override void onCreateMainItems()
        {
            this._ucDjrq = new UcDateInput();
            this._ucDjrq.CName = "单据日期";
            this._ucDjrq.AllowEmpty = false;
            this.controls.Add(this._ucDjrq);

            this._uXmmc = new UcTextInput();
            this._uXmmc.CName = "项目名称";
            this._uXmmc.AllowEmpty = false;
            this.controls.Add(this._uXmmc);

			this._ucJtsp = new UcCheckedInput("0,否;1,行管委;2,审监委", "-1", false);
			this._ucJtsp.CName = "集团审批";
			this._ucJtsp.AllowEmpty = false;
			this.controls.Add(this._ucJtsp);

			this._ucFjxm = new UcCheckedInput("1,比价单;100,其他", "-1", true);
			this._ucFjxm.CName = "附件项目";
			this._ucFjxm.AllowEmpty = false;
			this.controls.Add(this._ucFjxm);

			this._ucDxsm = new UcTextInput();
			this._ucDxsm.CName = "定向说明";
			this.controls.Add(this._ucDxsm);

			this._ucBjyj = new UcTextInput();
			this._ucBjyj.CName = "报价意见";
			this._ucBjyj.AllowEmpty = false;
			this.controls.Add(this._ucBjyj);

			this._ucBjje = new UcTextInput();
			this._ucBjje.CName = "报价金额";
			this._ucBjje.AllowEmpty = false;
			this.controls.Add(this._ucBjje);

			this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

            this._ucDetail = new UcJbBjdxbjjls(this.PP.detailTitle);
            this._ucDetail.AllowEmpty = false;
            this.controls.Add(this._ucDetail);
            this.Children.Insert(1, this._ucDetail);

        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("DjId");
            this._ucDjrq.ControlValue = data.GetValueByLabel("Djrq");
            this._uXmmc.ControlValue = data.GetValueByLabel("Xmmc");
            this._ucJtsp.ControlValue = data.GetValueByLabel("Jtsp");
			this._ucFjxm.ControlValue = data.GetValueByLabel("Fjxm");
			this._ucDxsm.ControlValue = data.GetValueByLabel("Dxsm");
			this._ucBjyj.ControlValue = data.GetValueByLabel("Bjyj");
			this._ucBjje.ControlValue = data.GetValueByLabel("Bjje");
			this._ucBz.ControlValue = data.GetValueByLabel("Bz");
            this._ucDetail.ControlValue = data.GetValueByLabel("StrMx");

        }

        protected override async Task<string> update()
        {
			this.uniqueId = await ((JbcmpWSUtil)GetWSUtil()).UpdateJbBjdxpsb(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._ucDjrq.ControlValue,
                this._uXmmc.ControlValue,
                this._ucJtsp.ControlValue,
				this._ucFjxm.ControlValue,
                this._ucDxsm.ControlValue,
				this._ucBjyj.ControlValue,
				this._ucBjje.ControlValue,
                this._ucBz.ControlValue,
                this._ucDetail.ControlValue,
                this.iNewRecode);

            return "项目更新成功。";
        }

        private class UcJbBjdxbjjls : UcDetailPage_Item
        {
            public UcJbBjdxbjjls(string title) : base(title) { }

            protected override HsLabelValue createLabelAndValue(HsLabelValue item)
            {
				string bjdw = item.GetValueByLabel("Bjdw");

                string je = item.GetValueByLabel("Je");

                item.Label = bjdw;
                item.Value = je;

                return item;
            }

            protected override async Task addItem()
            {
                Panel_JbBjdxbjjl panel = new Panel_JbBjdxbjjl();

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
                Panel_JbBjdxbjjl panel = new Panel_JbBjdxbjjl(item) { AuditOnly = !AllowEdit };

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

        private class Panel_JbBjdxbjjl : Panel_DJDetail
        {
			private UcTextInput _ucWlmc;

			private UcTextInput _ucBjdw;

			private UcTextInput _ucLxfs;

			private UcTextInput _ucGgxh;

            private UcTextInput _ucSl;

            private UcTextInput _ucDj;

            private UcTextInput _ucJe;

			private UcTextInput _ucBz;


            public Panel_JbBjdxbjjl(HsLabelValue item = null) : base(item) { }

            protected override void onInit()
            {
                base.onInit();

                titleSaved = "报价记录";
            }


            protected override void onCreateMainItems()
            {
                this._ucWlmc = new UcTextInput();
                this._ucWlmc.CName = "物料名称";
                this._ucWlmc.AllowEmpty = true;
                this.controls.Add(this._ucWlmc);

                this._ucBjdw = new UcTextInput();
                this._ucBjdw.CName = "报价单位";
				this._ucBjdw.AllowEmpty = false;
                this.controls.Add(this._ucBjdw);

				this._ucLxfs = new UcTextInput();
				this._ucLxfs.CName = "联系方式";
				this._ucLxfs.AllowEmpty = true;
				this.controls.Add(this._ucLxfs);

				this._ucGgxh = new UcTextInput();
                this._ucGgxh.CName = "规格型号";
                this._ucGgxh.AllowEmpty = true;
                this.controls.Add(this._ucGgxh);

                this._ucSl = new UcTextInput();
                this._ucSl.CName = "数量";
                this._ucSl.AllowEmpty = true;
                this.controls.Add(this._ucSl);

                this._ucDj = new UcTextInput();
                this._ucDj.CName = "单价";
                this._ucDj.AllowEmpty = true;
                this.controls.Add(this._ucDj);

                this._ucJe = new UcTextInput();
                this._ucJe.CName = "金额";
				this._ucJe.AllowEmpty = false;
                this.controls.Add(this._ucJe);

                this._ucBz = new UcTextInput();
                this._ucBz.CName = "备注";
                this._ucBz.AllowEmpty = true;
                this.controls.Add(this._ucBz);

            }

            protected override void setData(HsLabelValue data)
            {
                this._ucWlmc.ControlValue = data.GetValueByLabel("Wlmc");
                this._ucBjdw.ControlValue = data.GetValueByLabel("Bjdw");
                this._ucGgxh.ControlValue = data.GetValueByLabel("Ggxh");
                this._ucSl.ControlValue = data.GetValueByLabel("Sl");
                this._ucDj.ControlValue = data.GetValueByLabel("Dj");
                this._ucJe.ControlValue = data.GetValueByLabel("Je");
				this._ucLxfs.ControlValue = data.GetValueByLabel("Lxfs");
                this._ucBz.ControlValue = data.GetValueByLabel("Bz");
            }

            protected override async Task<string> update()
            {
                HsLabelValue item = new HsLabelValue();

                item.AddItem(new HsLabelValue() { Label = "Wlmc", Value = this._ucWlmc.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Bjdw", Value = this._ucBjdw.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Ggxh", Value = this._ucGgxh.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Sl", Value = this._ucSl.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Dj", Value = this._ucDj.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Je", Value = this._ucJe.ControlValue });
				item.AddItem(new HsLabelValue() { Label = "Lxfs", Value = this._ucLxfs.ControlValue });
                item.AddItem(new HsLabelValue() { Label = "Bz", Value = this._ucBz.ControlValue });

                this.updateCompleteData = item;

                return await Task.FromResult("");
            }
        }
    }
}
