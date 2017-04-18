using Hungsum.Framework.Exceptions;
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

            lv.ItemTapped += new EventHandler<ItemTappedEventArgs>((sender, e) =>
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

        protected virtual IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            return new List<MenuItem>();
        }

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
                });

                return viewCell;
            });

            dt.SetBinding(UcHsLabelValueCell.HsLabelValueProperty, new Binding("."));



            return dt;
        }

        #region 新建

        protected async Task callNew()
        {
            await addItem();
        }

        protected virtual async Task addItem()
        {
            await Task.FromResult("");
        }

        #endregion

        #region 修改

        protected virtual async void itemClick(object item)
        {
            try
            {
                await modifyItem(item as HsLabelValue);
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

        protected virtual async Task modifyItem(HsLabelValue item)
        {
            await Task.FromResult("");
        }

        #endregion

        #region DoData

        protected virtual async Task<string> callDoData(HsActionKey actionKey, HsLabelValue item)
        {
            if (item == null)
            {
                throw new HsException("选中数据不是有效的HsLabelValue对象。");
            }

            bool result = await this.DisplayAlert($"是否{actionKey.Label}数据？", "", "是", "否");

            if (result)
            {
                return await this.doDataItem(actionKey, item);
            }
            else
            {
                return await Task.FromResult("");
            }
        }

        protected virtual async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            await Task.Delay(1);

            throw new HsException($"未知的ActionKey【{actionKey}】");
        }

        #endregion

        #region ICommand

        public override async void Execute(object parameter)
        {
            try
            {
                var cp = parameter as HsCommandParams;

                if (cp != null)
                {
                    await this.callAction(cp.ActionKey, cp.Data as HsLabelValue);
                }
                else
                {
                    base.Execute(parameter);
                }
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }
        }

        protected virtual async Task callAction(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.新建)
            {
                await callNew();
            }
            else
            {
                await callDoData(actionKey, item);
            }
        }

        #endregion

    }
}
