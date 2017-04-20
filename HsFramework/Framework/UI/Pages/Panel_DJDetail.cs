using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hungsum.Framework.Models;
using Hungsum.Framework.Events;

namespace Hungsum.Framework.UI.Pages
{
    public abstract class Panel_DJDetail : Panel_DJ
    {
        public Panel_DJDetail(HsLabelValue item = null) : base(item) { }
    }
}
