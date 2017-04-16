using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Events;
using Hungsum.Framework.UI.Pages;

namespace App8.Framework.UI.Pages
{
    public abstract class UcDJDetailPage : UcContentPage, IControlValue
    {
        #region IControlValue

        public string CName { get; set; }

        public string ControlId { get; set; }

        public abstract string ControlType
        {
            get;
        }

        public string ControlLabel => CName;

        public string ControlValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool AllowEdit { get; set; }

        public bool AllowEmpty { get; set; }

        public event EventHandler<HsEventArgs<string>> DataChanged;

        event EventHandler<HsEventArgs<string>> IControlValue.DataChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void onDataChanged()
        {
            this.DataChanged?.Invoke(this, new HsEventArgs<string>());
        }

        public abstract void Validate();

        public abstract void Reset();

        #endregion
    }
}
