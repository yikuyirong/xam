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
    public sealed class UcQueryResultPage : UcListPage
    {
        private List<HsLabelValue> _items;

        public UcQueryResultPage(List<HsLabelValue> items) : base()
        {
            _items = items;

            this.lv.HasUnevenRows = true;
        }

        #region UcListPage

        protected override async void onInit()
        {
            base.onInit();

            this.lv.ItemsSource = await retrieve();
        }

        protected override async Task<List<HsLabelValue>> retrieve()
        {
            return await Task.FromResult(_items);

            //await Task.Delay(1);

            //return _items;
        }

        protected override async void itemClick(object item)
        {
            try
            {
                HsLabelValue lv = item as HsLabelValue;

                if (lv != null && lv.Items.Count > 0)
                {
                    UcQueryResultPage page = new UcQueryResultPage(lv.Items);

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
