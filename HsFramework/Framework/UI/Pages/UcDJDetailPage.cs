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

namespace Hungsum.Framework.UI.Pages
{
    public abstract class UcDJDetailPage : UcListPage, IControlValue
    {
        protected ObservableCollection<HsLabelValue> datas;

        public UcDJDetailPage()
        {
            this.datas = new ObservableCollection<HsLabelValue>();

            this.datas.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) =>
            {
                this.onDataChanged();
            });

            this.lv.ItemsSource = datas;

            this.AllowEdit = true;
        }


        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            items.Add(new MenuItem()
            {
                Text = "删除",
                Command = this,
                CommandParameter = new HsCommandParams(SysActionKeys.删除, item),
                IsDestructive = true
            });

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

        #region IControlValue

        public string CName { get; set; }

        public string ControlId { get; set; }

        public string ControlType => Views.ControlType.Detail;

        public string ControlLabel => CName;

        public abstract string ControlValue { get; set; }

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
    }
}
