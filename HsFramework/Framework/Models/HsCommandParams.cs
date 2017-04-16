using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hungsum.Framework.Models
{
    public class HsCommandParams
    {
        public readonly HsActionKey ActionKey;

        public readonly object Data;

        public HsCommandParams(HsActionKey actionKey, object data = null)
        {
            this.ActionKey = actionKey;
            this.Data = data;
        }
    }
}
