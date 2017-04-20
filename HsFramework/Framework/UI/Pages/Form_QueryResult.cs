using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;
using Hungsum.Framework.UI.Cells;

namespace Hungsum.Framework.UI.Pages
{
    public sealed class Form_QueryResult : Form_Base
    {
        private List<HsLabelValue> _items;

        public Form_QueryResult(List<HsLabelValue> items) : base()
        {
            _items = items;

            this.lv.HasUnevenRows = true;
        }

        #region UcListPage

        protected override void onInit()
        {
            base.onInit();

            this.lv.ItemsSource = _items;
        }

        protected override async void itemClick(object item)
        {
            try
            {
                HsLabelValue lv = item as HsLabelValue;

                if (lv != null && lv.Items.Count > 0)
                {
                    Form_QueryResult page = new Form_QueryResult(lv.Items);

                    await Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }


        #endregion

    }
}
