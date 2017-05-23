using FormsPlugin.Iconize;
using Hungsum.Framework.Events;
using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Pages;
using Hungsum.Framework.UI.Views;
using Hungsum.Sdrd.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hungsum.Sdrd.UI.Page
{
	public class Form_Sdrdxm_Operation : Form_DJ
	{
		public override string GetTitle()
		{
			return "项目维护";
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

			if (item.GetValueByLabel("Xmzt") == "0")
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


		protected override async Task<List<HsLabelValue>> retrieve()
		{
			return await ((SdrdWSUtil)GetWSUtil()).ShowXms(GetLoginData().ProgressId,
			                                               this.ucBeginDate.ControlValue,
			                                               this.ucEndDate.ControlValue,
			                                               this.ucUserSwitcher.ControlValue,
			                                               "0,1",
			                                               GetLoginData().Username);
		}

		protected override async Task addItem()
		{
			Panel_Sdrdxm panel = new Panel_Sdrdxm();

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


		protected async override Task modifyItem(HsLabelValue item)
		{
			Panel_Sdrdxm panel = new Panel_Sdrdxm(item);

			if (item.GetValueByLabel("Xmzt") == "0")
			{
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
			}
			else
			{
				panel.AuditOnly = true;
			}

			await Navigation.PushAsync(panel);
		}

		protected override async Task<string> doDataItem(HsActionKey actionKey, HsLabelValue item)
		{
			if (actionKey == SysActionKeys.删除)
			{
				return await this.callRemoteDoData(item, "Delete_Xm");
			}
			else
			{
				return await base.doDataItem(actionKey, item);
			}
		}

	}
}
