using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Events;

namespace Hungsum.Framework.UI.Views
{
    public class UcTextInput : Entry,IControlValue
    {
        public UcTextInput()
        {
            AllowEdit = true;
            AllowEmpty = true;

            this.TextChanged += textChangedEventHandler;
        }

        protected virtual void textChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != e.OldTextValue)
            {
                this.onDataChanged(e.NewTextValue);
            }
        }

        protected void onDataChanged(string data)
        {
            this.DataChanged?.Invoke(this, new HsEventArgs<string>() { Data = data });
        }


        #region 实现IControlValue

        public event EventHandler<HsEventArgs<string>> DataChanged;

        public string CName { get; set; }

        public string ControlId { get; set; }

        public string ControlType { get { return Views.ControlType.TextInput; } }

        public string ControlLabel { get; set; }

        public virtual string ControlValue
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public bool AllowEdit
        {
            get { return this.IsEnabled; }
            set { this.IsEnabled = value; }
            
        }
        public bool AllowEmpty { get; set; }

        public virtual void Reset()
        {
            ControlValue = string.Empty;
        }

        public virtual void Validate()
        {
            if (!AllowEmpty && string.IsNullOrWhiteSpace(ControlValue))
            {
                throw new HsException($"{CName}不能为空");
            }
        }

    #endregion
    }
}
