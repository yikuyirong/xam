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
    public class Panel_Sdrdlxr : UcDJPage
    {

        private HsLabelValue _kh;

        private UcTextInput _ucName;

        private UcTextInput _ucZwzc;

        private UcCheckedInput _ucSex;

        private UcDateInput _ucBirthday;

        private UcTextInput _ucPhone;

        private UcTextInput _ucPhone2;

        private UcTextInput _ucEmail;

        private UcTextInput _ucQQ;

        private UcTextInput _ucWe;

        private UcTextInput _ucHobby;

        private UcTextInput _ucBz;

        public Panel_Sdrdlxr(HsLabelValue kh, HsLabelValue item = null) : base(item)
        {
            this._kh = kh;
        }

        protected override void onInit()
        {
            base.onInit();

            this.titleSaved = "联系人维护";
        }

        protected override void onCreateMainItems()
        {
            this._ucName = new UcTextInput();
            this._ucName.CName = "姓名";
            this._ucName.AllowEmpty = false;
            this.controls.Add(this._ucName);

            this._ucZwzc = new UcTextInput();
            this._ucZwzc.CName = "职务职称";
            this.controls.Add(this._ucZwzc);

            this._ucSex = new UcCheckedInput("0,男;1,女", "0");
            this._ucSex.CName = "性别";
            this.controls.Add(this._ucSex);

            this._ucBirthday = new UcDateInput();
            this._ucBirthday.CName = "生日";
            this.controls.Add(this._ucBirthday);

            this._ucPhone = new UcTextInput();
            this._ucPhone.CName = "电话";
            this.controls.Add(this._ucPhone);

            this._ucPhone2 = new UcTextInput();
            this._ucPhone2.CName = "电话2";
            this.controls.Add(this._ucPhone2);

            this._ucEmail = new UcTextInput();
            this._ucEmail.CName = "电子邮件";
            this.controls.Add(this._ucEmail);

            this._ucQQ = new UcTextInput();
            this._ucQQ.CName = "QQ";
            this.controls.Add(this._ucQQ);

            this._ucWe = new UcTextInput();
            this._ucWe.CName = "微信";
            this.controls.Add(this._ucWe);

            this._ucHobby = new UcTextInput();
            this._ucHobby.CName = "爱好";
            this.controls.Add(this._ucHobby);

            this._ucBz = new UcTextInput();
            this._ucBz.CName = "备注";
            this.controls.Add(this._ucBz);

        }

        protected override void setData(HsLabelValue data)
        {
            this.uniqueId = data.GetValueByLabel("LxrId");

            _ucName.ControlValue = data.GetValueByLabel("Name");

            _ucZwzc.ControlValue = data.GetValueByLabel("Zwzc");

            _ucSex.ControlValue = data.GetValueByLabel("Sex");

            _ucBirthday.ControlValue = data.GetValueByLabel("Birthday");

            _ucPhone.ControlValue = data.GetValueByLabel("Phone");

            _ucPhone2.ControlValue = data.GetValueByLabel("Phone2");

            _ucEmail.ControlValue = data.GetValueByLabel("Email");

            _ucQQ.ControlValue = data.GetValueByLabel("QQ");

            _ucWe.ControlValue = data.GetValueByLabel("We");

            _ucHobby.ControlValue = data.GetValueByLabel("Hobby");

            _ucBz.ControlValue = data.GetValueByLabel("Bz");
        }

        protected override async Task<string> update()
        {
            return await ((SdrdWSUtil)GetWSUtil()).UpdateLxr(
                GetLoginData().ProgressId,
                this.uniqueId,
                this._kh.GetValueByLabel("KhId"),
                this._ucName.ControlValue,
                this._ucZwzc.ControlValue,
                this._ucSex.ControlValue,
                this._ucBirthday.ControlValue,
                this._ucPhone.ControlValue,
                this._ucPhone2.ControlValue,
                this._ucEmail.ControlValue,
                this._ucQQ.ControlValue,
                this._ucWe.ControlValue,
                this._ucHobby.ControlValue,
                this._ucBz.ControlValue,
                this.iNewRecode);
        }
    }
}
