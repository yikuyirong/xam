using Hungsum.Framework.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hungsum.Framework.Models;
using Hungsum.Sdrd.Models;

namespace Hungsum.Sdrd.UI.Page
{
    public class Form_SdrdMain : UcMainPage
    {
        public Form_SdrdMain(XElement xMenus) : base(xMenus) { }


        protected override async void doAction(IHsLabelValue item)
        {
            try
            {
                switch (item.Value)
                {
                    case SdrdFuncKey.RD客户维护:
                        await Navigation.PushAsync(new Form_Sdrdkh_Operation());
                        break;
                    case SdrdFuncKey.RD客户交流记录维护:
                        await Navigation.PushAsync(new Form_Sdrdkhjljl_Operation());
                        break;
                    case SdrdFuncKey.RD客户交流记录浏览:
                        await Navigation.PushAsync(new Form_Sdrdkhjljl_Query());
                        break;
                    case SdrdFuncKey.RD项目维护:
                        await Navigation.PushAsync(new Form_Sdrdxm_Operation());
                        break;
                    case SdrdFuncKey.RD合同维护:
                        await Navigation.PushAsync(new Form_Sdrdht_Operation());
                        break;
                    case SdrdFuncKey.RD合同确认:
                        await Navigation.PushAsync(new Form_Sdrdht_Confirm());
                        break;
                    case SdrdFuncKey.RD合同回款记录维护:
                        await Navigation.PushAsync(new Form_Sdrdhthkjl_Operation());
                        break;
                    case SdrdFuncKey.RD合同考核记录维护:
                        await Navigation.PushAsync(new Form_Sdrdhtkhjl_Operation());
                        break;
                    default:
                        base.doAction(item);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }

        }
    }
}
