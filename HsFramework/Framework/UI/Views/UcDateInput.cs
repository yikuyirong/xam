using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Acr.UserDialogs;

using Hungsum.Framework.Extentsions;

namespace Hungsum.Framework.UI.Views
{
    public class UcDateInput : UcSelectInput
    {

        public const string NOW = "NOW";

        public const string MONTHFIRST = "MONTHFIRST";

        public const string YEARFIRST = "YEARFIRST";

        private DateTime? _selectDate = null;

        private DateTime _maxDate = DateTime.MaxValue;

        private DateTime _minDate = DateTime.MinValue;

        public DateTime MaxDate
        {
            set { _maxDate = value; }
        }

        public DateTime MinDate
        {
            set { _minDate = value; }
        }


        public string Flag
        {
            set
            {
                if (value != null)
                {
                    switch (value.ToUpper())
                    {
                        case NOW:
                            ControlValue = DateTime.Today.ToString();
                            break;
                        case MONTHFIRST:
                            ControlValue = DateTime.Today.GetMonthFirst().ToString();
                            break;
                        case YEARFIRST:
                            ControlValue = DateTime.Today.GetYearFirst().ToString();
                            break;
                    }
                }
            }
        }

        public override string ControlType => Views.ControlType.DateInput;

        public override string ControlLabel => throw new NotImplementedException();

        public override string ControlValue
        {
            get { return this.textInput.ControlValue; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.textInput.ControlValue = string.Empty;

                    _selectDate = null;
                }
                else
                {
                    DateTime d;

                    if (DateTime.TryParse(value, out d))
                    {
                        this.textInput.ControlValue = d.ToString("yyyy-MM-dd");

                        _selectDate = d;
                    }
                }
            }
        }

        protected override void pushSelectDataPage()
        {

            UserDialogs.Instance.DatePrompt(new DatePromptConfig()
            {
                OkText = "确定",
                CancelText = "重置",
                IsCancellable = true,
                MaximumDate = _maxDate,
                MinimumDate = _minDate,
                SelectedDate = _selectDate,
                OnAction = new Action<DatePromptResult>((result) =>
                {
                    if (result.Ok)
                    {
                        ControlValue = result.Value.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        ControlValue = string.Empty;
                    }
                })
            });
        }
    }
}
