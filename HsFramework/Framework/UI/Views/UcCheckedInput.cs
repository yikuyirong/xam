using Acr.UserDialogs;

using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.Utilities;
using Rg.Plugins.Popup.Services;

using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Views
{
    public sealed class UcCheckedInput : UcSelectInput
    {
        private bool _multiChoice;

        public UcCheckedInput(string datas = "0,否;1,是", string defaultValue = "", bool multiChoice = false)
        {
            this._multiChoice = multiChoice;

            this.Datas = datas;

            ControlValue = defaultValue == null ? "" : defaultValue;

        }

        public string Datas
        {
            set
            {
                this.datas.Clear();

                foreach (string data in value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] lvs = data.Split(',');

                    this.datas.Add(new HsLabelValue() { Value = lvs[0], Label = lvs[1] });
                }
            }
        }


        public override string ControlType => Views.ControlType.RadioBox;

        private string _controlValue;

        public override string ControlValue
        {
            get => this._controlValue;

            set
            {
                _controlValue = value;

                string[] vs = value.Split(',');

                this.textInput.ControlValue = string.Join(",", this.datas.Where(r => vs.Contains(r.Value)).Select(r => r.Label));
            }
        }

        public override string ControlLabel => this.textInput.ControlValue;

        protected override async void pushSelectDataPage()
        {
            try
            {
                CheckedDataPopupPage page = new CheckedDataPopupPage(CName, this.datas, this._multiChoice, ControlValue);

                page.PopupData += new EventHandler<HsEventArgs<HsActionKey, string>>((sender, e) =>
                {
                    ControlValue = e.Data2;
                });

                await PopupNavigation.PushAsync(page);
            }
            catch (Exception e)
            {
                UserDialogs.Instance.ShowError(e.Message);
            }
        }

        public class CheckedDataPopupPage : UcNormalPopupPage<string>
        {
            private List<UcChooseItemBase> _controls;

            public CheckedDataPopupPage(string cName, IEnumerable<HsLabelValue> datas, bool multiChoice = false, string value = "1")
            {
                this.Title = $"请选择{cName}";

                string[] values = value.Split(',');

                //Controls

                _controls = new List<UcChooseItemBase>();

                foreach (HsLabelValue item in datas)
                {
                    UcChooseItemBase ucChooseItem;

                    if (multiChoice)
                    {
                        ucChooseItem = new UcCheckBox { CName = item.Label, ControlValue = item.Value };
                    } else
                    {
                        ucChooseItem = new UcRadioBox { CName = item.Label, ControlValue = item.Value };
                    }

                    ucChooseItem.Checked = values.Where(r => r == item.Value).Count() > 0;

                    ucChooseItem.DataChanged += new EventHandler<HsEventArgs<string>>((sender, e) =>
                    {
                        if (!multiChoice && ((UcChooseItemBase)sender).Checked)
                        {
                            foreach (UcChooseItemBase c in _controls.Where(r => r.ControlValue != item.Value))
                            {
                                c.Checked = false;
                            }
                        }
                    });

                    _controls.Add(ucChooseItem);
                }

                foreach (UcChooseItemBase control in _controls)
                {
                    mainLayout.Children.Add(new UcFormItem(control));
                }

            }

            protected override async void callAction(HsActionKey actionKey, object item)
            {
                try
                {
                    if (actionKey == MenuItemKeys.UserDo1)
                    {
                        this.onPopupData(MenuItemKeys.选择数据, string.Join(",", _controls.Where(r => r.Checked).Select(r => r.ControlValue)));

                        await PopupNavigation.PopAsync();
                    }
                    else if (actionKey == MenuItemKeys.UserDo2)
                    {
                        await PopupNavigation.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            }
        }
    }
}
