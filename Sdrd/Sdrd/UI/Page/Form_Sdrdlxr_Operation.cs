using FormsPlugin.Iconize;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdlxr_Operation : UcDJListPage
    {
        private HsLabelValue _kh;

        public Form_Sdrdlxr_Operation(HsLabelValue kh)
        {
            this._kh = kh;

            this.uniqueIdField = "LxrId";
        }

        public override string GetTitle()
        {
            return $"{this._kh.GetValueByLabel("Khmc")}联系人";
        }

        protected override IList<ToolbarItem> OnCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.OnCreateToolbarItems();

            items.Insert(0, new IconToolbarItem()
            {
                Icon = "ion-plus-round",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.新建)
            });

            return items;
        }

        protected override IList<MenuItem> onCreateContextMenuItems(HsLabelValue item)
        {
            IList<MenuItem> items = base.onCreateContextMenuItems(item);

            items.Add(new MenuItem()
            {
                Text = "删除",
                Command = this,
                CommandParameter = new HsCommandParams(MenuItemKeys.删除, item),
                IsDestructive = true
            });

            return items;
        }


        protected override async Task<List<HsLabelValue>> retrieve()
        {
            string khId = this._kh.GetValueByLabel("KhId");

            return await ((SdrdWSUtil)GetWSUtil()).ShowLxrs(GetLoginData().ProgressId, khId);
        }

        protected override async void addItem()
        {
            try
            {
                Panel_Sdrdlxr panel = new Panel_Sdrdlxr(this._kh);

                panel.UpdateComplete += new EventHandler((sender, e) =>
                {
                    this.callRetrieve(false);
                });

                await Navigation.PushAsync(panel);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }


        protected async override void modifyItem(HsLabelValue item)
        {
            try
            {
                Panel_Sdrdlxr panel = new Panel_Sdrdlxr(this._kh, item);

                panel.UpdateComplete += new EventHandler((sender, e) =>
                {
                    this.callRetrieve(false);
                });

                await Navigation.PushAsync(panel);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == MenuItemKeys.删除)
            {
                return await this.doData(item, "Delete_Lxr");
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }
    }
}
