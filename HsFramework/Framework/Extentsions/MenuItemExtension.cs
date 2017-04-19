using Hungsum.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.Extentsions
{
    public static class MenuItemExtension
    {
        public static HsActionKey GetActionKey(this MenuItem item)
        {
            HsCommandParams cp = item.CommandParameter as HsCommandParams;

            return cp?.ActionKey;
        }

        public static bool ExistIn(this MenuItem item, IEnumerable<HsActionKey> actionKeys)
        {
            foreach (HsActionKey key in actionKeys)
            {
                if (item.GetActionKey() == key)
                {
                    return true;
                }

                continue;
            }

            return false;
        }


    }
}
