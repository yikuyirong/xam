using Hungsum.Framework.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Events;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Specialized;
using Hungsum.Framework.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcDetailPage_Item : Form_Base, IControlValue
    {
        protected ObservableCollection<HsLabelValue> datas;

        public UcDetailPage_Item(string title)
        {

            CName = title;

            this.datas = new ObservableCollection<HsLabelValue>();

            this.datas.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) =>
            {
                this.onDataChanged();
            });

            this.lv.ItemsSource = datas;

            this.AllowEdit = true; //初始值为真

            #region 标题栏

            UcHeaderTitle headerTitle = new UcHeaderTitle(title);

            #endregion

            #region 控制栏

            StackLayout controlLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White
            };

            controlLayout.Children.Add(
                new Button()
                {
                    Text = "添加明细",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.新建),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                });

            #endregion

            this.mainLayout.Children.Insert(0, headerTitle);

            this.mainLayout.Children.Insert(1, controlLayout);
        }


        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            if (AllowEdit)
            {
                items.Add(new MenuItem()
                {
                    Text = "删除",
                    Command = this,
                    CommandParameter = new HsCommandParams(SysActionKeys.删除, item),
                    IsDestructive = true
                });
            }

            return items;
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.删除)
            {
                this.datas.Remove(item);
                return "";
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }

        /// <summary>
        /// 根据items集合内的数据补全Label与Value值，返回本身即可。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract HsLabelValue createLabelAndValue(HsLabelValue item);

        #region IControlValue

        public string CName { get; set; }

        public string ControlId { get; set; }

        public string ControlType => Views.ControlType.Detail;

        public string ControlLabel => CName;

        public virtual string ControlValue
        {
            get
            {
                JArray container = new JArray();

                foreach (HsLabelValue lv in datas)
                {
                    JObject jObj = new JObject();

                    foreach (HsLabelValue item in lv.Items)
                    {
                        jObj.Add(item.Label, JValue.CreateString(item.Value));
                    }

                    container.Add(jObj);
                }

                return JsonConvert.SerializeObject(container);
            }

            set
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        JContainer jContainer = JsonConvert.DeserializeObject(value) as JContainer;

                        if (jContainer != null)
                        {
                            foreach (JObject jObj in jContainer)
                            {
                                HsLabelValue item = new HsLabelValue();

                                foreach (KeyValuePair<string, JToken> jKv in jObj)
                                {
                                    item.AddItem(new HsLabelValue() { Label = jKv.Key, Value = jKv.Value.ToString() });
                                }

                                datas.Add(createLabelAndValue(item));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }

            }
        }

        private bool _allowEdit = true;

        public bool AllowEdit
        {
            get
            {
                return _allowEdit;
            }
            set
            {
                _allowEdit = value;

                this.onCanExecuteChanged();
            }
        }

        public bool AllowEmpty { get; set; }

        public event EventHandler<HsEventArgs<string>> DataChanged;

        public void onDataChanged()
        {
            this.DataChanged?.Invoke(this, new HsEventArgs<string>());
        }

        public virtual void Validate()
        {
            if (!AllowEmpty && this.datas.Count < 1)
            {
                throw new HsException($"{CName}不能为空");
            }
        }

        public virtual void Reset()
        {
            this.datas.Clear();
        }

        #endregion

        #region ICommand

        public override bool CanExecute(object parameter)
        {
            return AllowEdit;
        }

        #endregion
    }
}
