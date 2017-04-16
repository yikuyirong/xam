using Acr.UserDialogs;

using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.Extentsions;

using Rg.Plugins.Popup.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Hungsum.Framework.Events;
using Hungsum.Framework.UI.Cells;
using System.Collections.ObjectModel;
using Hungsum.Framework.App;
using Hungsum.Framework.Utilities;
using FormsPlugin.Iconize;

namespace Hungsum.Framework.UI.Views
{
    public sealed class UcAutoCompleteInput : UcSelectInput
    {
        private string _flag;

        private string _args;

        private bool _forceRetrieve = false;


        private bool _isRetrieve = false;

        public UcAutoCompleteInput(string flag, string args = "", bool forceRetrieve = false)
        {
            _flag = flag;

            _args = args;

            this._forceRetrieve = forceRetrieve;
        }

        public override string ControlType => Views.ControlType.AutoComplete;

        private string _controlValue;

        public override string ControlValue
        {
            get => this._controlValue;

            set
            {
                string[] lvs = value.Split(',');

                HsLabelValue lv = null;

                if (lvs.Length == 1)
                {
                    if (string.IsNullOrWhiteSpace(lvs[0]))
                    {
                        lv = new HsLabelValue() { Value = "", Label = "" };
                    }
                    else
                    {
                        lv = this.datas.First(r => r.Value == lvs[0]);
                    }
                }
                else if (lvs.Length == 2)
                {
                    lv = new HsLabelValue() { Value = lvs[0], Label = lvs[1] };
                }

                if (lv == null)
                {
                    throw new HsException($"{CName}的值{value}无效");
                }

                _controlValue = lv.Value;

                this.textInput.ControlValue = lv.Label;
            }
        }

        public override string ControlLabel => this.textInput.ControlValue;

        protected override void pushSelectDataPage()
        {
            _pushSelectDataPage(this._forceRetrieve);
        }

        private async void _pushSelectDataPage(bool forceRetrieve)
        {
            try
            {
                //检查是否更新过数据。
                if (!forceRetrieve && this._isRetrieve)
                {
                    AutoCompleteDataPopupPage page = new AutoCompleteDataPopupPage(CName, this.datas);

                    page.PopupData += new EventHandler<HsEventArgs<HsActionKey, string>>((sender,e)=>
                    {
                        ControlValue = e.Data2;
                    }); 

                    //page.DataSelected += new EventHandler<Events.HsEventArgs<string>>((sender, e) =>
                    //{
                    //    ControlValue = e.Data;
                    //});

                    await PopupNavigation.PushAsync(page);
                }
                else
                {
                    //获取数据
                    UserDialogs.Instance.ShowLoading("载入中...");

                    HsApp app = ((HsApp)Application.Current);

                    string result = await app.WSUtil.GetAutoCompleteData(app.LoginData.ProgressId, _flag, _args);

                    foreach (XElement xItem in XElement.Parse(result).Elements("Item"))
                    {
                        this.datas.Add(new HsLabelValue() { Label = xItem.GetFirstElementValue("Label"), Value = xItem.GetFirstElementValue("Value") });
                    }


                    this._isRetrieve = true;

                    UserDialogs.Instance.HideLoading();

                    //
                    _pushSelectDataPage(false);
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.ShowError(e.Message);
            }

        }

        /// <summary>
        /// 模拟测试数据
        /// </summary>
        /// <returns></returns>
        private async Task<List<HsLabelValue>> _simulatorData()
        {
            List<HsLabelValue> items = new List<HsLabelValue>();

            for (int i = 0; i < 20; i++)
            {
                items.Add(new HsLabelValue() { Label = $"Label_{i}", Value = $"Value_{i}" });
            }

            await Task.Delay(2000);

            return items;
        }

        public class AutoCompleteDataPopupPage : UcNormalPopupPage<string>
        {
            public AutoCompleteDataPopupPage(string cName, IEnumerable<HsLabelValue> datas)
            {
                ObservableCollection<HsLabelValue> displayItems = new ObservableCollection<HsLabelValue>(datas);

                this.Title = $"请选择{cName}";

                //ControlButton

                btnUserDo1.Text = "重置";
                btnUserDo2.Text = "取消";


                //Listview

                SearchBar search2 = new SearchBar() { Placeholder = "输入关键字筛选..." };

                search2.TextChanged += new EventHandler<TextChangedEventArgs>((sender, e) =>
                {
                    displayItems.Clear();

                    string pattern = e.NewTextValue;

                    foreach (HsLabelValue data in datas.Where(r => r.Label.Contains(pattern) || r.Value.Contains(pattern) || string.IsNullOrWhiteSpace(pattern)))
                    {
                        displayItems.Add(data);
                    }
                });

                UcTextInput search = new UcTextInput() { Placeholder = "输入关键字筛选...", Margin = new Thickness(5) };

                search.DataChanged += new EventHandler<HsEventArgs<string>>((sender, e) =>
                {
                    displayItems.Clear();

                    string pattern = e.Data;

                    foreach (HsLabelValue data in datas.Where(r => r.Label.Contains(pattern) || r.Value.Contains(pattern) || string.IsNullOrWhiteSpace(pattern)))
                    {
                        displayItems.Add(data);
                    }
                });

                ListView lv = new ListView() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };

                lv.ItemsSource = displayItems;

                DataTemplate dt = new DataTemplate(typeof(UcIHsLabelValueCell));

                dt.SetBinding(UcIHsLabelValueCell.IHsLabelValueProperty, new Binding("."));

                lv.ItemTemplate = dt;

                lv.ItemSelected += new EventHandler<SelectedItemChangedEventArgs>(async (sender, e) =>
                {
                    if (e.SelectedItem != null)
                    {
                        this.onPopupData(MenuItemKeys.选择数据, ((HsLabelValue)e.SelectedItem).Value);

                        await PopupNavigation.PopAsync();
                    }

                });

                lv.ItemTapped += new EventHandler<ItemTappedEventArgs>((sender, e) =>
                {
                    lv.SelectedItem = null;
                });

                mainLayout.Children.Add(search2);
                mainLayout.Children.Add(lv);








            }

            protected override async void callAction(HsActionKey actionKey, object item)
            {
                try
                {
                    if (actionKey == MenuItemKeys.UserDo1) //重置
                    {
                        this.onPopupData(MenuItemKeys.选择数据, string.Empty);

                        await PopupNavigation.PopAsync();
                    }
                    else if (actionKey == MenuItemKeys.UserDo2) //取消
                    {
                        await PopupNavigation.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            }
        }

    }
}
