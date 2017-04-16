using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Events;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Views
{
    public class UcTimeInput : TimePicker, IControlValue
    {
        protected void onDataChanged(string data)
        {
            this.DataChanged?.Invoke(this, new HsEventArgs<string>() { Data = data });
        }


        #region 实现IControlValue

        public string CName { get; set; }

        public string ControlId { get; set; }

        public string ControlType { get { return Views.ControlType.TimeInput; } }

        public string ControlLabel { get; set; }

        public string ControlValue
        {
            get { return this.Time.Ticks.ToString(); }
            set
            {
                long ticks = long.Parse(value);
                if (ticks != this.Time.Ticks)
                {
                    this.Time = new TimeSpan(ticks);

                    this.onDataChanged(ticks.ToString());
                }
            }
        }

        public bool AllowEdit
        {
            get { return this.IsEnabled; }
            set { this.IsEnabled = value; }
        }

        public bool AllowEmpty { get; set; }

        public event EventHandler<HsEventArgs<string>> DataChanged;

        public void Reset() { this.ControlValue = "0"; }

        public void Validate() { }

        #endregion
    }
}
