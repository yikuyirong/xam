using Hungsum.Framework.Events;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Models;

namespace Hungsum.OA.Workflow.UI.Page
{
    public class Panel_HsLcspjl : UcDJPage
    {
        //private var _ucLczy:UcTextInput = new UcTextInput();

        //private var _ucBzmc:UcTextInput = new UcTextInput();

        //private var _ucDwmc:UcTextInput = new UcTextInput();

        //private var _ucRolemc:UcTextInput = new UcTextInput();

        //private var _ucZdr:UcTextInput = new UcTextInput();

        //protected var ucSpyj:UcTextInput = new UcTextInput();

        //protected var ucJlzt:UcMultiRadio = new UcMultiRadio("1,同意;2,不同意");

        //protected var ucZdthsp:UcAutoComplete;

        private UcTextInput _ucLczy;

        private UcTextInput _ucBzmc;

        private UcTextInput _ucDwmc;

        private UcTextInput _ucRolemc;

        private UcTextInput _ucZdr;

        private UcTextInput _ucSpyj;

        private UcCheckedInput _ucJlzt;

        private UcAutoCompleteInput _ucZdthsp;

        public Panel_HsLcspjl()
        {
            this._ucLczy.CName = "流程摘要";
            this._ucLczy.AllowEmpty = false;
            this._ucLczy.AllowEdit = false;
            this.controls.Add(this._ucLczy);

            this._ucBzmc.CName = "步骤名称";
            this._ucBzmc.AllowEmpty = false;
            this._ucBzmc.AllowEdit = false;
            this.controls.Add(this._ucBzmc);

            this._ucDwmc.CName = "审批人部门";
            this._ucDwmc.AllowEmpty = true;
            this._ucDwmc.AllowEdit = false;
            this.controls.Add(this._ucDwmc);

            this._ucRolemc.CName = "审批人角色";
            this._ucRolemc.AllowEmpty = true;
            this._ucRolemc.AllowEdit = false;
            this.controls.Add(this._ucRolemc);

            this._ucZdr.CName = "审批人";
            this._ucZdr.AllowEmpty = false;
            this._ucZdr.AllowEdit = false;
            this.controls.Add(this._ucZdr);

            this._ucSpyj.CName = "审批意见";
            this._ucSpyj.AllowEmpty = true;
            this.controls.Add(this._ucSpyj);

            this._ucJlzt.CName = "审批状态";
            this._ucJlzt.AllowEmpty = false;
            this._ucJlzt.DataChanged += new EventHandler<HsEventArgs<string>>((sender, e) =>
            {
            }); // .addEventListener(Event.CHANGE, this._jlztChangedHandler, false, 0, true);
            this.controls.Add(this._ucJlzt);

            //this._ucZdthsp = new UcAutoCompleteInput("ZDTHSP", data["SpId"].toString(), true);
            //this._ucZdthsp.CName = "退回至";
            //this._ucZdthsp.toolTip = "如果留空，则" + data["Bhclmc"].toString();
            //this._ucZdthsp.AllowEmpty = true;
            //this._ucZdthsp.AllowEdit = false;
            //this._ucZdthsp.ControlValue = " ";
            //this.controls.addItem(this._ucZdthsp);
            //var f_zdthsp:UcFormItem = new UcFormItem(this._ucZdthsp);

        }

        protected override void onCreateMainItems()
        {
            throw new NotImplementedException();
        }

        protected override void setData(HsLabelValue data)
        {
            throw new NotImplementedException();
        }

        protected override Task<string> update()
        {
            throw new NotImplementedException();
        }
    }
}
