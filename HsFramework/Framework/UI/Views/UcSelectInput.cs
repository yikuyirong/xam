using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Events;
using Xamarin.Forms;
using Hungsum.Framework.Exceptions;

using Acr.UserDialogs;
using Hungsum.Framework.Models;

namespace Hungsum.Framework.UI.Views
{
    public abstract class UcSelectInput : AbsoluteLayout, IControlValue
    {
        protected List<HsLabelValue> datas = new List<HsLabelValue>();

        protected UcTextInput textInput;

        protected Button button;

        public UcSelectInput()
        {
            Padding = new Thickness(0);

            //this.HeightRequest = Device.GetNamedSize(NamedSize.Large, typeof(Editor));
            this.HeightRequest = 30;

            textInput = new UcTextInput();
            AbsoluteLayout.SetLayoutBounds(textInput, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(textInput, AbsoluteLayoutFlags.All);

            textInput.DataChanged += new EventHandler<HsEventArgs<string>>((sender,e) =>
            {
                this.DataChanged?.Invoke(this, new HsEventArgs<string>() { Data = ControlValue });
            });

            button = new Button(){ BackgroundColor = Color.Transparent };
            AbsoluteLayout.SetLayoutBounds(button, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(button, AbsoluteLayoutFlags.All);

            button.Clicked += clickEventHandler;

            Children.Add(textInput);

            Children.Add(button);
        }

        protected virtual void clickEventHandler(object sender, EventArgs e)
        {
            //UserDialogs.Instance.ShowError("请在子类中实现[clickEventHandler]方法");

            this.pushSelectDataPage();
        }

        protected abstract void pushSelectDataPage();

        #region 实现IControlValue

        public string CName
        {
            get { return this.textInput.CName; }
            set { this.textInput.CName = value; }
        }

        public string ControlId
        {
            get { return this.textInput.ControlId; }
            set { this.textInput.ControlId = value; }
        }

        public abstract string ControlType { get; }

        public abstract string ControlLabel { get;}

        public abstract string ControlValue { get; set; }

        public bool AllowEdit
        {
            get { return this.textInput.AllowEdit; }
            set
            {
                this.textInput.AllowEdit = value;

                this.button.IsEnabled = value;
            }
        }

        /// <summary>
        /// 是否允许为空，默认为Ture
        /// </summary>
        public bool AllowEmpty
        {
            get { return this.textInput.AllowEmpty; }
            set { this.textInput.AllowEmpty = value; }
        }

        public event EventHandler<HsEventArgs<string>> DataChanged;

        public void Reset()
        {
            ControlValue = string.Empty;
        }

        public void Validate()
        {
            if (!AllowEmpty && string.IsNullOrWhiteSpace(ControlValue))
            {
                throw new HsException($"{CName}不能为空");
            }
        }

        #endregion
    }
}
