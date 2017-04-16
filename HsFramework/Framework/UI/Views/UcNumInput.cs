using Hungsum.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hungsum.Framework.Events;

namespace Hungsum.Framework.UI.Views
{
    public class UcNumInput : UcTextInput
    {

        public bool CanFushu { get; set; }

        public double _value;

        public double Value
        {
            get { return double.Parse(ControlValue); }
        }

        public UcNumInput()
        {
            Keyboard = Xamarin.Forms.Keyboard.Numeric;
        }

        protected override void textChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            /*
            double d;

            if (double.TryParse(e.NewTextValue, out d) || e.NewTextValue == string.Empty || (CanFushu && e.NewTextValue == "-"))
            {
                if (!CanFushu && d < 0)
                {
                    this.Text = e.OldTextValue;
                }
                else
                {
                    base.textChangedEventHandler(sender, e);
                }
            }
            else
            {
                this.Text = e.OldTextValue;
            }
            */
        }


        #region 实现IControlValue

        public override string ControlValue
        {
            get
            {
                return (string.IsNullOrWhiteSpace(this.Text) || this.Text == "-") ? "0" : this.Text.Trim();
            }
            set
            {
                double d;

                if (double.TryParse(value, out d))
                {
                    this.Text = value;
                }
                else
                {
                    this.Text = string.Empty;
                }
            }
        }

        public override void Validate()
        {
            double d;

            if (double.TryParse(this.Text, out d))
            {
                if (!AllowEmpty && d == 0)
                {
                    throw new HsException($"{CName}不能为零");
                }

                if (!CanFushu && d < 0)
                {
                    throw new HsException($"{CName}不能为负数");
                }
            }
            else
            {
                throw new HsException($"{CName}不是数字");
            }

        }

    #endregion
    }
}
