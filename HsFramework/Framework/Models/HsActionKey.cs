using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Models
{
    public class HsActionKey : IHsLabelValue, IEquatable<HsActionKey>
    {
        public string Label { get; set; }

        public string Value { get; set; }

        public HsActionKey(string value, string label)
        {
            this.Label = label;
            this.Value = value;
        }

        public HsActionKey SetLabel(string label)
        {
            this.Label = label;

            return this;
        }

        public bool Equals(HsActionKey other)
        {
            return Value == other.Value;
        }

        public override string ToString()
        {
            return $"{Label}-{Value}";
        }

    }
}
