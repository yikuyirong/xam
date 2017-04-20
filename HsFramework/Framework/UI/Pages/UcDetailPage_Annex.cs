using Hungsum.Framework.Events;
using Hungsum.Framework.Exceptions;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcDetailPage_Annex<T> : UcContentPage, IControlValue, IUcDJAnnexPage where T : class
    {
        protected bool hasRrtrieve = false;

        protected bool isRetrieveing = false;

        protected bool hasDataChanged = false;

        public abstract Task GetItems();

        public abstract Task UpdateItems();

        public string Djlx { get; set; }

        public string DjId { get; set; }

        protected ListView liveview;

        protected ObservableCollection<T> datas = new ObservableCollection<T>();

        public UcDetailPage_Annex(string title)
        {
            this.AllowEdit = true;
            this.AllowEmpty = true;

            #region 注册数据变化事件

            datas.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) =>
            {
                if (!this.isRetrieveing) //获取初始数据时不触发更改事件。
                {
                    this.onDataChanged();
                }
            });

            #endregion

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

            IEnumerable<HsActionKey> actionKeys = onCreateActionKeys();

            foreach (HsActionKey actionKey in actionKeys)
            {
                controlLayout.Children.Add(
                    new Button()
                    {
                        Text = actionKey.Label,
                        Command = this,
                        CommandParameter = new HsCommandParams(actionKey),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                    });
            }

            #endregion

            #region 图像列表

            liveview = new ListView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                //Opacity = 0.6,
                BackgroundColor = Color.Transparent,
                //RowHeight = 100
            };

            liveview.ItemTapped += new EventHandler<ItemTappedEventArgs>((sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            });

            liveview.ItemSelected += new EventHandler<SelectedItemChangedEventArgs>((sender, e) =>
            {
                try
                {
                    if (e.SelectedItem != null)
                    {
                        this.itemClick(e.SelectedItem as T);
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            });



            liveview.ItemTemplate = getDataTemplete();

            liveview.ItemsSource = datas;

            #endregion

            StackLayout rootLayout = new StackLayout() { Spacing = 0 };

            rootLayout.Children.Add(headerTitle);
            rootLayout.Children.Add(controlLayout);
            rootLayout.Children.Add(liveview);

            this.Content = rootLayout;

        }

        protected virtual IEnumerable<HsActionKey> onCreateActionKeys()
        {
            return new List<HsActionKey>();
        }

        protected virtual DataTemplate getDataTemplete()
        {
            return new DataTemplate();
        }

        protected virtual void itemClick(T item) { }


        #region IUcDJAnnexPage

        public bool HasRetrieve => hasRrtrieve;

        public bool HasDataChanged => hasDataChanged;

        #endregion

        #region IControlValue

        public string CName { get; set; }

        public string ControlId { get; set; }

        public abstract string ControlType
        {
            get;
        }

        public string ControlLabel => CName;

        public string ControlValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private bool _allowEdit;

        public bool AllowEdit
        {
            get { return _allowEdit; }
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
            this.hasDataChanged = true; //数据已经发生改变

            this.DataChanged?.Invoke(this, new HsEventArgs<string>());
        }

        public virtual void Validate()
        {
            if (!AllowEmpty && datas.Count < 1)
            {
                throw new HsException($"{CName}不能为空");
            }
        }

        public virtual void Reset()
        {
            datas.Clear();
        }

        #endregion

        #region ICommand

        public override bool CanExecute(object parameter)
        {
            return AllowEdit;
        }

        protected abstract void callAction(HsActionKey actionKey, object data);

        public override void Execute(object parameter)
        {
            try
            {
                HsCommandParams cp = parameter as HsCommandParams;

                if (cp == null)
                {
                    base.Execute(parameter);
                }
                else
                {
                    this.callAction(cp.ActionKey, cp.Data);
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
