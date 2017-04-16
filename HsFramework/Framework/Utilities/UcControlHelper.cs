using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Hungsum.Framework.UI.Views;
using Hungsum.Framework.Extentsions;
using Hungsum.Framework.UI.Pages;

namespace Hungsum.Framework.Utilities
{
    public class UcControlHelper
    {
        public static Tuple<IControlValue, int, int> CreateFromXQueryArg(IUcPage page, XElement xArg)
        {
            string name = xArg.GetFirstElementValue("Name");
            string cname = xArg.GetFirstElementValue("CName");
            string Class = xArg.GetFirstElementValue("Class").ToUpper();
            string classInfo = xArg.GetFirstElementValue("ClassInfo");
            string classParams = xArg.GetFirstElementValue("ClassParams");
            bool allowEmpty = xArg.GetFirstElementValue("AllowEmpty", "1") == "0" ? false : true;
            string defaultValue = xArg.GetFirstElementValue("Default");
            int order = int.Parse(xArg.GetFirstElementValue("Order", "0"));
            int sqlOrder = int.Parse(xArg.GetFirstElementValue("SqlOrder", "0"));

            IControlValue control = null;

            switch (xArg.Element("Class").Value.ToUpper())
            {
                case ControlType.TextInput:
                    control = new UcTextInput();
                    control.ControlValue = defaultValue;
                    break;
                case ControlType.TextArea:
                case "AREA":
                    control = new UcTextArea();
                    control.ControlValue = defaultValue;
                    break;
                case ControlType.NumInput:
                case "NUMBER":
                case "NUMBERINPUT":
                    control = new UcNumInput();
                    control.ControlValue = defaultValue;
                    break;
                case ControlType.CheckBox:
                case "CHECK":
                    control = new UcCheckedInput(classInfo, defaultValue, true);
                    break;
                case ControlType.RadioBox:
                case "RADIO":
                    control = new UcCheckedInput(classInfo, defaultValue, false);
                    break;
                case ControlType.AutoComplete:
                case "AUTO":
                case "AUTOCOMPLETETEXT":
                case "AUTOCOMPLETEINPUT":
                    control = new UcAutoCompleteInput(classInfo, classParams, false);
                    break;
                case ControlType.DateInput:
                    control = new UcDateInput();
                    ((UcDateInput)control).Flag = classInfo;
                    break;
            }

            control.CName = cname;
            control.AllowEmpty = allowEmpty;

            return new Tuple<IControlValue, int, int>(control, order, sqlOrder);
        }
    }
}
