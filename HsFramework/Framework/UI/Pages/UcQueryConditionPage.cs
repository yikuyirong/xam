using Hungsum.Framework.Models;
using Hungsum.Framework.UI.Views;
using Hungsum.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
    public class UcQueryConditionPage : UcZDPage
    {
        private XElement _xQuery;

        private List<Tuple<IControlValue, int, int>> _tControls;

        private string _queryName = null;

        public UcQueryConditionPage(XElement xQuery) : base()
        {
            this._xQuery = xQuery;

            this.Title = xQuery.Element("QueryTitle").Value;

            this._queryName = xQuery.Element("QueryName").Value;
        }

        public override string GetTitle()
        {
            return this._xQuery.Element("QueryTitle").Value;
        }

        protected override void onCreateMainItems()
        {
            this._tControls = new List<Tuple<IControlValue, int, int>>();

            foreach (XElement xArg in this._xQuery.Element("QueryArgs").Elements("Arg"))
            {
                Tuple<IControlValue, int, int> tControl = UcControlHelper.CreateFromXQueryArg(this,xArg);

                this._tControls.Add(tControl);
            }

            foreach (var tControl in this._tControls.OrderBy(r => r.Item2))
            {
                controls.Add(tControl.Item1);
            }
        }

        protected override IList<ToolbarItem> onCreateToolbarItems()
        {
            IList<ToolbarItem> items = base.onCreateToolbarItems();

            items.First(r => ((HsCommandParams)r.CommandParameter)?.ActionKey == MenuItemKeys.确定).Text = "查询";

            return items;
        }

        protected override async Task<string> update()
        {
            XElement xArgs = new XElement("Args", from r in this._tControls select
                                                  new XElement("Arg",
                                                    new XElement("Value", r.Item1.ControlValue),
                                                    new XElement("SqlOrder", r.Item3)));

            List<HsLabelValue> items = await this.GetWSUtil().QueryResult(GetLoginData().ProgressId, this._queryName, xArgs);

            UcQueryResultPage page = new UcQueryResultPage(items);

            await Navigation.PushAsync(page);

            return null;
        }
    }
}
