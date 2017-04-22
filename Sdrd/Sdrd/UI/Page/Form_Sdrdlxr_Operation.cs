using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_Sdrdlxr_Operation : Form_DJ
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
                CommandParameter = new HsCommandParams(SysActionKeys.新建)
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
                CommandParameter = new HsCommandParams(SysActionKeys.删除, item),
                IsDestructive = true
            });

            return items;
        }


        protected override async Task<List<HsLabelValue>> retrieve()
        {
            string khId = this._kh.GetValueByLabel("KhId");

            return await ((SdrdWSUtil)GetWSUtil()).ShowLxrs(GetLoginData().ProgressId, khId);
        }

        protected override async Task addItem()
        {
            Panel_Sdrdlxr panel = new Panel_Sdrdlxr(this._kh);

            panel.UpdateComplete += new EventHandler<HsEventArgs<object>>(async (sender, e) =>
           {
               try
               {
                   await this.callRetrieve(false);
               }
               catch (Exception ex)
               {
                   this.ShowError(ex.Message);
               }
           });

            await Navigation.PushAsync(panel);
        }


        protected override async Task modifyItem(HsLabelValue item)
        {
            Panel_Sdrdlxr panel = new Panel_Sdrdlxr(this._kh, item);

            panel.UpdateComplete += new EventHandler<HsEventArgs<object>>(async (sender, e) =>
            {
                try
                {
                    await this.callRetrieve(false);
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            });

            await Navigation.PushAsync(panel);
        }

        protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
        {
            if (actionKey == SysActionKeys.删除)
            {
                return await this.callRemoteDoData(item, "Delete_Lxr");
            }
            else
            {
                return await base.doDataItem(actionKey, item);
            }
        }
    }
}
