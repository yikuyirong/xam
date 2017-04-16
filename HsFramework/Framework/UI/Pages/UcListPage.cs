using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Cells;
using Hungsum.Framework.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcListPage : UcContentPage
    {
        protected ListView lv;

        public UcListPage()
        {
            this.lv = new ListView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsGroupingEnabled = false,
                BackgroundColor = Color.Transparent,
                IsPullToRefreshEnabled = false,
                IsRefreshing = false,
                HasUnevenRows = true
            };

            lv.ItemTemplate = getItemTemplate();

            lv.ItemTapped += new EventHandler<ItemTappedEventArgs>((sender,e) =>
            {
                ((ListView)sender).SelectedItem = null;
            });

            lv.ItemSelected += new EventHandler<SelectedItemChangedEventArgs>((sender, e) =>
            {
                try
                {
                    if (e.SelectedItem != null)
                    {
                        this.itemClick(e.SelectedItem);
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            });

            Content = lv;
        }


        protected override void onInit()
        {
            base.onInit();

        }

        public override string GetBackgroundImage()
        {
            return HsDevice.OnPlatform<string>("background.png", "tilebackground.xml");
        }

        protected virtual IList<MenuItem> onCreateContextMenuItems(HsLabelValue viewCell)
        {
            return new List<MenuItem>();
        }

        protected abstract Task<List<HsLabelValue>> retrieve();

        protected DataTemplate getItemTemplate()
        {
            DataTemplate dt = new DataTemplate(() =>
            {
                UcHsLabelValueCell viewCell = new UcHsLabelValueCell();

                viewCell.BindingContextChanged += new EventHandler((sender, e) =>
                {
                    UcHsLabelValueCell cell = (UcHsLabelValueCell)sender;

                    if (cell.HsLabelValue != null)
                    {
                        cell.ContextActions.Clear();

                        foreach (MenuItem mItem in onCreateContextMenuItems(cell.HsLabelValue))
                        {
                            cell.ContextActions.Add(mItem);
                        }
                    }

                    //if (viewCell.RequestHeight > lv.RowHeight)
                    //{
                    //    lv.RowHeight = (int)viewCell.Height;
                    //}

                });

                return viewCell;
            });

            dt.SetBinding(UcHsLabelValueCell.HsLabelValueProperty, new Binding("."));

            

            return dt;
        }

        protected abstract void itemClick(object item);


    }
}
