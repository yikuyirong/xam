using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Events;
using Xamarin.Forms;
using FormsPlugin.Iconize;

namespace Hungsum.Framework.UI.Views
{
    public abstract class UcChooseItemBase : StackLayout, IControlValue
    {
        private IconButton _checkButton;

        protected string checkIcon = "ion-android-checkbox-outline"; 

        protected string unCheckIcon = "ion-android-checkbox-outline-blank";

        public UcChooseItemBase()
        {
            _checkButton = new IconButton()
            {
                FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Button)),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            _checkButton.Clicked += new EventHandler((sender, e) =>
            {
                Checked = !Checked;

                this.DataChanged?.Invoke(this, new HsEventArgs<string>() { Data = ControlValue });
            });

            Checked = false;

            this.Children.Add(_checkButton);
        }

        private bool _checked;

        public bool Checked
        {
            get
            {
                return _checked;
            }

            set
            {
                _checked = value;

                //_checkButton.TextColor = value ? _defaultColor : Color.Transparent;
                
                _checkButton.Text = value ? checkIcon : unCheckIcon;
            }
        }

        #region

        public string CName { get; set; }

        public string ControlId { get; set; }


        public string ControlType => Views.ControlType.Switch;

        public string ControlLabel => ControlValue;

        //public string ControlValue
        //{
        //    get { return this._switch.IsToggled ? "1" : "0"; }
        //    set
        //    {
        //        this._switch.IsToggled = value == "1";
        //    }
        //}

        public string ControlValue { get; set; }

        public bool AllowEdit
        {
            get { return this.IsEnabled; }
            set { this.IsEnabled = value; }
        }

        public bool AllowEmpty
        {
            get { return true; }
            set { }
        }

        public event EventHandler<HsEventArgs<string>> DataChanged;

        public void Reset() { }

        public void Validate() { }

        #endregion

    }
}
