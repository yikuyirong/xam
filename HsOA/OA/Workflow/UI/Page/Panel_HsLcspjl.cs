using Hungsum.Framework.Events;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Models;
using Hungsum.Jbcmp.OA.Workflow;
using Hungsum.Jbcmp.OA.Models;
using Hungsum.OA.Utilities;
using Hungsum.Framework.Exceptions;

namespace Hungsum.OA.Workflow.UI.Page
{
    public class Panel_HsLcspjl : Panel_DJ
    {
        private bool _hasNextBz = false;

        private string _lcStyle;

        private string _djlx;

        private UcTextInput _ucLczy;

        private UcTextInput _ucBzmc;

        private UcTextInput _ucDwmc;

        private UcTextInput _ucRolemc;

        private UcTextInput _ucZdr;

        private UcTextInput _ucSpyj;

        private UcCheckedInput _ucJlzt;

        private UcAutoCompleteInput _ucZdthsp;

        public Panel_HsLcspjl(HsLabelValue lcspjl) : base(lcspjl)
        {
            //通过模版Id判断流程类型
            this._lcStyle = lcspjl.GetValueByLabel("MbId") != "0" ? ELcStyle.固定流程 : ELcStyle.自由流程;

            this.PP = new PageParams() { ImageTitle = "审批图片", detailTitle = "审批附件" };

        }

        protected override void onInit()
        {
            base.onInit();

            this.Djlx = HsOADjlx.SYSLCSPJL;

            titleSaved = "流程审批记录";
        }

        protected override void onCreateMainItems()
        {
            this._ucLczy = new UcTextInput();
            this._ucLczy.CName = "流程摘要";
            this._ucLczy.AllowEmpty = false;
            this._ucLczy.AllowEdit = false;
            this.controls.Add(this._ucLczy);

            this._ucBzmc = new UcTextInput();
            this._ucBzmc.CName = "步骤名称";
            this._ucBzmc.AllowEmpty = false;
            this._ucBzmc.AllowEdit = false;
            this.controls.Add(this._ucBzmc);

            this._ucDwmc = new UcTextInput();
            this._ucDwmc.CName = "审批人部门";
            this._ucDwmc.AllowEmpty = true;
            this._ucDwmc.AllowEdit = false;
            this.controls.Add(this._ucDwmc);

            this._ucRolemc = new UcTextInput();
            this._ucRolemc.CName = "审批人角色";
            this._ucRolemc.AllowEmpty = true;
            this._ucRolemc.AllowEdit = false;
            this.controls.Add(this._ucRolemc);

            this._ucZdr = new UcTextInput();
            this._ucZdr.CName = "审批人";
            this._ucZdr.AllowEmpty = false;
            this._ucZdr.AllowEdit = false;
            this.controls.Add(this._ucZdr);

            this._ucSpyj = new UcTextInput();
            this._ucSpyj.CName = "审批意见";
            this._ucSpyj.AllowEmpty = true;
            this.controls.Add(this._ucSpyj);

            this._ucJlzt = new UcCheckedInput("1,同意;2,不同意", "", false);
            this._ucJlzt.CName = "审批状态";
            this._ucJlzt.AllowEmpty = false;
            this._ucJlzt.DataChanged += new EventHandler<HsEventArgs<string>>((sender, e) =>
            {
                if (e.Data == "1")
                {
                    this._ucZdthsp.Reset();
                    this._ucZdthsp.AllowEdit = false;
                }
                else
                {
                    this._ucZdthsp.AllowEdit = true;
                }
            });
            this.controls.Add(this._ucJlzt);

            this._ucZdthsp = new UcAutoCompleteInput("ZDTHSP", this.userData.GetValueByLabel("SpId"), true);
            this._ucZdthsp.CName = "退回至";
            this._ucZdthsp.AllowEmpty = true;
            this._ucZdthsp.AllowEdit = false;
            this.controls.Add(this._ucZdthsp);
        }

        protected override void setData(HsLabelValue data)
        {

            this.uniqueId = data.GetValueByLabel("JlId");
            this._djlx = data.GetValueByLabel("Djlx");
            this._ucLczy.ControlValue = data.GetValueByLabel("Lczy");
            this._ucLczy.AllowEdit = false;
            this._ucBzmc.ControlValue = data.GetValueByLabel("Bzmc");
            this._ucBzmc.AllowEdit = false;
            this._ucDwmc.ControlValue = data.GetValueByLabel("Dwmc");
            this._ucDwmc.AllowEdit = false;
            this._ucRolemc.ControlValue = data.GetValueByLabel("Rolemc");
            this._ucRolemc.AllowEdit = false;
            this._ucZdr.ControlValue = data.GetValueByLabel("Zdr");
            this._ucZdr.AllowEdit = false;
            this._ucSpyj.ControlValue = data.GetValueByLabel("Spyj");

            if (data.GetValueByLabel("Bzlx") == ELcbzlx.通知确认类)
            {
                this._ucSpyj.Reset();
                this._ucSpyj.AllowEmpty = false;
                this._ucJlzt.ControlValue = "1"; //通知确认类步骤直接置为同意
                this._ucJlzt.IsVisible = false;
            }
            else
            {
                this._ucSpyj.AllowEmpty = true;
                this._ucJlzt.ControlValue = data.GetValueByLabel("Jlzt");
            }


            //只有一退的审批方式才显示退会审批
            if (new string[] { ELcbzspfs.一进全退, ELcbzspfs.一进半退, ELcbzspfs.半进半退 }.FirstOrDefault(r => r == data.GetValueByLabel("Spfs")) != null)
            {
                this._ucZdthsp.IsVisible = false;
            }
        }

        protected override async Task<string> update()
        {
            if (this._lcStyle == ELcStyle.固定流程 || this._hasNextBz || this._ucJlzt.ControlValue == "0")
            {
                this.uniqueId = await ((HsOAWSUtil)GetWSUtil()).UpdateHsLcspjl(
                    GetLoginData().ProgressId,
                    this.uniqueId,
                    this._ucSpyj.ControlValue,
                    this._ucJlzt.ControlValue,
                    this._ucZdthsp.ControlValue);

                return "审批成功。";
            }
            else
            {
                List<HsLabelValue> items = await ((HsOAWSUtil)GetWSUtil()).ShowHsLcbzfzs(
                    GetLoginData().ProgressId,
                    this.uniqueId,
                    this._ucSpyj.ControlValue,
                    this._ucJlzt.ControlValue,
                    this._ucZdthsp.ControlValue);

                if (items.Count == 0)
                {
                    throw new HsException("目前系统不能处理未明确分支的流程。");
                }
                else
                {
                    this._hasNextBz = true;

                    return await this.update();
                }
            }
        }
    }
}
