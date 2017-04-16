using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hungsum.Framework.Extentsions
{
    public static class XElementExtension
    {
        public static string GetFirstElementValue(this XElement xData,string name,string defaultValue = "")
        {
            XElement xValue = xData.Element(name);

            if (xValue != null)
            {
                return xValue.Value;
            }
            else
            {
                return defaultValue;
            }
        }

        public static HsLabelValue ToHsLabelValue(this XElement xData)
        {
            HsLabelValue item = new HsLabelValue();

            item.Label = xData.GetFirstElementValue("Label");
            item.LabelColor = int.Parse(xData.GetFirstElementValue("ItemColor", "0")).ToXamarinColor();
            item.Value = xData.GetFirstElementValue("Value");
            item.ValueColor = int.Parse(xData.GetFirstElementValue("ItemColor", "0")).ToXamarinColor();
            item.ItemShowDetailImage = bool.Parse(xData.GetFirstElementValue("ItemShowDetailImage", false.ToString()));

            foreach (XElement xSubItem in xData.Elements())
            {
                string label = xSubItem.Name.LocalName;
                if (!label.ExEquals("Label", "Value", "ItemColor", "ItemShowDetailImage"))
                {
                    item.AddItem(new HsLabelValue() { Label = label, Value = xSubItem.Value });
                }
            }

            return item;
        }
    }
}
