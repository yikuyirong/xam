using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.Models
{
    public class HsLabelValue : IHsLabelValue
    {
        public string Label { get; set; }

        public Color LabelColor { get; set; }

        public string Value { get; set; }

        public Color ValueColor { get; set; }

        public bool ItemShowDetailImage { get; set; }

        public bool IsEmpty
        {
            get { return string.IsNullOrWhiteSpace(Value); }
        }

        private List<HsLabelValue> _items = new List<HsLabelValue>();

        public List<HsLabelValue> Items
        {
            get { return _items; }
        }

        public void AddItem(HsLabelValue item)
        {
            _items.Add(item);
        }

        public void AddItems(IEnumerable<HsLabelValue> items)
        {
            _items.AddRange(items);
        }

        public string GetValueByLabel(string label, string defaultValue = "")
        {
            IHsLabelValue item = _items.Find(r => r.Label == label);

            return item == null ? defaultValue : item.Value;
        }
    }
}
