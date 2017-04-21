
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.Jbcmp.Models;
using Hungsum.Jbcmp.Utilities;

using System.Threading.Tasks;

namespace Hungsum.Jbcmp.UI.Page
{
    public class Panel_JbHtpsb : Panel_DJ
    {
        private UcAutoCompleteInput _ucJgqrd;

        private UcDateInput _ucDjrq;

        private UcTextInput _ucHtdw;

        private UcTextInput _ucHtmc;

        private UcTextInput _ucHtbh;

        private UcCheckedInput _ucJgly;

        private UcCheckedInput _ucJtsp;

        private UcCheckedInput _ucFjxm;

        private UcNumInput _ucHtje;

        private UcTextInput _ucFkfs;

        private UcTextInput _ucHtqx;

        private UcTextArea _ucZytk;

        private UcTextInput _ucBz;

        public Panel_JbHtpsb(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = JbcmpDjlx.JBHTPSB;

            titleSaved = JbcmpDjlx.JB合同评审表;
        }

        protected override void onCreateMainItems()
        {
            this._ucJgqrd = new UcAutoCompleteInput("JBJGQRD");
            this._ucJgqrd.CName = "价格确认信息";
            this._ucJgqrd.AllowEmpty = true;
            this.controls.Add(this._ucJgqrd);

            this._ucDjrq = new UcDateInput();
            this._ucDjrq.CName = "单据日期";
            this._ucDjrq.AllowEmpty = false;
            this.controls.Add(this._ucDjrq);

            this._ucHtdw = new UcTextInput();
            this._ucHtdw.CName = "合同单位";
            this._ucHtdw.AllowEmpty = false;
            this.controls.Add(this._ucHtdw);

            this._ucHtmc = new UcTextInput();
            this._ucHtmc.CName = "合同名称";
            this._ucHtmc.AllowEmpty = false;
            this.controls.Add(this._ucHtmc);

            this._ucHtbh = new UcTextInput();
            this._ucHtbh.CName = "合同编号";
            this._ucHtbh.AllowEmpty = false;
            this.controls.Add(this._ucHtbh);

            this._ucJgly = new UcCheckedInput("0,招标;1,比价或定向;2,参照招标价议价", "0");
            this._ucJgly.CName = "价格来源";
            this._ucJgly.AllowEmpty = false;
            this.controls.Add(this._ucJgly);

            this._ucJtsp = new UcCheckedInput("0,否;1,行管委;2,审监委", "");
            this._ucJtsp.CName = "集团审批";
            this._ucJtsp.AllowEmpty = false;
            this.controls.Add(this._ucJtsp);

            this._ucFjxm = new UcCheckedInput("1,合同文本草稿;2,合同价明细;3,图纸;4,中标通知书;5,中标单位确认单;6,投标文件;8,比价审批单;7,其他", "1");
            this._ucFjxm.CName = "附件项目";
            this._ucFjxm.AllowEmpty = false;
            this.controls.Add(this._ucFjxm);

            this._ucHtje = new UcNumInput();
            this._ucHtje.CName = "合同金额";
            this._ucHtje.AllowEmpty = false;
            this.controls.Add(this._ucHtje);

            this._ucFkfs = new UcTextInput();
            this._ucFkfs.CName = "付款方式";
            this._ucFkfs.AllowEmpty = false;
            this.controls.Add(this._ucFkfs);

            this._ucHtqx = new UcTextInput();
            this._ucHtqx.CName = "合同期限";
            this._ucHtqx.AllowEmpty = false;
            this.controls.Add(this._ucHtqx);

            this._ucZytk = new UcTextArea();
            this._ucZytk.CName = "主要条款";
            this._ucZytk.AllowEmpty = true;
            this.controls.Add(this._ucZytk);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this._ucBz.AllowEmpty = true;
            this.controls.Add(this._ucBz);
        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("DjId");
            this._ucJgqrd.ControlValue = $"{data.GetValueByLabel("QrdId")},{data.GetValueByLabel("QrdXmmc")}";
            this._ucDjrq.ControlValue = data.GetValueByLabel("Djrq");
            this._ucHtdw.ControlValue = data.GetValueByLabel("Htdw");
            this._ucHtmc.ControlValue = data.GetValueByLabel("Htmc");
            this._ucHtbh.ControlValue = data.GetValueByLabel("Htbh");
            this._ucJgly.ControlValue = data.GetValueByLabel("Jgly");
            this._ucJtsp.ControlValue = data.GetValueByLabel("Jtsp");
            this._ucFjxm.ControlValue = data.GetValueByLabel("Fjxm");
            this._ucHtje.ControlValue = data.GetValueByLabel("Htje");
            this._ucFkfs.ControlValue = data.GetValueByLabel("Fkfs");
            this._ucHtqx.ControlValue = data.GetValueByLabel("Htqx");
            this._ucZytk.ControlValue = data.GetValueByLabel("Zytk");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");
        }

        protected override async Task<string> update()
        {
            return await ((JbcmpWSUtil)GetWSUtil()).UpdateJbHtpsb(GetLoginData().ProgressId,
                                            this.uniqueId,
                                            this._ucJgqrd.ControlValue,
                                            this._ucDjrq.ControlValue,
                                            this._ucHtdw.ControlValue,
                                            this._ucHtmc.ControlValue,
                                            this._ucHtbh.ControlValue,
                                            this._ucJgly.ControlValue,
                                            this._ucJtsp.ControlValue,
                                            this._ucFjxm.ControlValue,
                                            this._ucHtje.ControlValue,
                                            this._ucFkfs.ControlValue,
                                            this._ucHtqx.ControlValue,
                                            this._ucZytk.ControlValue,
                                            this._ucBz.ControlValue,
                                            this.iNewRecode);
        }
    }
}
