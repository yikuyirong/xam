
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.Jbcmp.Models;
using Hungsum.Jbcmp.Utilities;

using System.Threading.Tasks;

namespace Hungsum.Jbcmp.UI.Page
{
    public class Panel_JbZbwjpsb : Panel_DJ
    {
        private UcDateInput _ucDjrq;

        private UcTextInput _ucXmmc;

        private UcTextInput _ucWjbh;

        private UcCheckedInput _ucJtsp;

        private UcNumInput _ucYsje;

        private UcCheckedInput _ucFjxm;

        private UcCheckedInput _ucSfjg;

        private UcTextInput _ucJgsm;

        private UcTextArea _ucQtsm;

        private UcTextInput _ucBz;

        public Panel_JbZbwjpsb(HsLabelValue item = null) : base(item)
        {
            this.PP = new PageParams();
        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = JbcmpDjlx.JBZBWJPSB;

            titleSaved = JbcmpDjlx.JB招标文件评审表;
        }

        protected override void onCreateMainItems()
        {
            this._ucDjrq = new UcDateInput();
            this._ucDjrq.CName = "单据日期";
            this._ucDjrq.AllowEmpty = false;
            this.controls.Add(this._ucDjrq);

            this._ucXmmc = new UcTextInput();
            this._ucXmmc.CName = "项目名称";
            this._ucXmmc.AllowEmpty = false;
            this.controls.Add(this._ucXmmc);

            this._ucWjbh = new UcTextInput();
            this._ucWjbh.CName = "文件编号";
            this._ucWjbh.AllowEmpty = false;
            this.controls.Add(this._ucWjbh);

            this._ucJtsp = new UcCheckedInput("0,否;1,行管委;2,审监委", "-1");
            this._ucJtsp.CName = "集团审批";
            this._ucJtsp.AllowEmpty = false;
            this.controls.Add(this._ucJtsp);

            this._ucYsje = new UcNumInput();
            this._ucYsje.CName = "预算金额";
            this._ucYsje.AllowEmpty = false;
            this.controls.Add(this._ucYsje);

            this._ucFjxm = new UcCheckedInput("1,招标文件草稿;2,图纸;100,其他", "1");
            this._ucFjxm.CName = "附件项目";
            this._ucFjxm.AllowEmpty = false;
            this.controls.Add(this._ucFjxm);

            this._ucSfjg = new UcCheckedInput();
            this._ucSfjg.CName = "是否有技改计划";
            this._ucSfjg.AllowEmpty = false;
            this.controls.Add(this._ucSfjg);

            this._ucJgsm = new UcTextInput();
            this._ucJgsm.CName = "技改说明";
            this._ucJgsm.AllowEmpty = true;
            this.controls.Add(this._ucJgsm);

            this._ucQtsm = new UcTextArea();
            this._ucQtsm.CName = "其他说明";
            this._ucQtsm.AllowEmpty = true;
            this.controls.Add(this._ucQtsm);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this._ucBz.AllowEmpty = true;
            this.controls.Add(this._ucBz);
        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("DjId");
            this._ucDjrq.ControlValue = data.GetValueByLabel("Djrq");
            this._ucXmmc.ControlValue = data.GetValueByLabel("Xmmc");
            this._ucWjbh.ControlValue = data.GetValueByLabel("Wjbh");
            this._ucJtsp.ControlValue = data.GetValueByLabel("Jtsp");
            this._ucYsje.ControlValue = data.GetValueByLabel("Ysje");
            this._ucFjxm.ControlValue = data.GetValueByLabel("Fjxm");
            this._ucSfjg.ControlValue = data.GetValueByLabel("Sfjg");
            this._ucJgsm.ControlValue = data.GetValueByLabel("Jgsm");
            this._ucQtsm.ControlValue = data.GetValueByLabel("Qtsm");
            this._ucBz.ControlValue = data.GetValueByLabel("Bz");
        }

        protected override void validate()
        {
            base.validate();

            if (this._ucSfjg.ControlValue == "0" && this._ucJgsm.ControlValue == "")
            {
                throw new HsException("无技改计划的招标请填写技改说明。");
            }
        }

        protected override async Task<string> update()
        {
            return await ((JbcmpWSUtil)GetWSUtil()).UpdateJbZbwjpsb(GetLoginData().ProgressId,
                                            this.uniqueId,
                                            this._ucDjrq.ControlValue, 
                                            this._ucXmmc.ControlValue, 
                                            this._ucWjbh.ControlValue, 
                                            this._ucJtsp.ControlValue, 
                                            this._ucYsje.ControlValue, 
                                            this._ucFjxm.ControlValue, 
                                            this._ucSfjg.ControlValue, 
                                            this._ucJgsm.ControlValue, 
                                            this._ucQtsm.ControlValue,
                                            this._ucBz.ControlValue,
                                            this.iNewRecode);
        }
    }
}
