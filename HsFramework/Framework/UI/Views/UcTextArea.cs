using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Events;
using Xamarin.Forms;
using Hungsum.Framework.Exceptions;

namespace Hungsum.Framework.UI.Views
{
    public class UcTextArea : Editor, IControlValue
    {
        public UcTextArea()
        {
            AllowEmpty = true;

            this.TextChanged += new EventHandler<TextChangedEventArgs>((sender, e) =>
            {
                if (e.NewTextValue != e.OldTextValue)
                {
                    this.onDataChanged(e.NewTextValue);
                }
            });
        }

        protected void onDataChanged(string data)
        {
            this.DataChanged?.Invoke(this, new HsEventArgs<string>() { Data = data});
        }

        #region 实现IControlValue

        public string CName { get; set; }

        public string ControlId { get; set; }

        public string ControlType { get { return Views.ControlType.TextArea; } }

        public string ControlLabel { get; set; }

        public string ControlValue
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

        public event EventHandler<HsEventArgs<string>> DataChanged;

        public void Reset()
        {
            this.ControlValue = string.Empty;
        }

        public void Validate()
        {
            if (!this.AllowEmpty && string.IsNullOrWhiteSpace(this.ControlValue))
            {
                throw new HsException($"{CName}不能为空");
            }
        }

        #endregion
    }
}
